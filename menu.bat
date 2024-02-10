@echo off
cls
:menu
cls

date /t

echo Computador: %computername%        Usuario: %username%
echo:         
echo            MENU TAREFAS
echo  ==================================
echo * 1. Levantar no docker            * 
echo * 2. Criar migrations              *
echo * 3. Aplicar migrations            *
echo * 0. Sair                          * 
echo  ==================================

set /p opcao= Escolha uma opcao: 
echo ------------------------------
if %opcao% equ 1 goto opcao1
if %opcao% equ 2 goto opcao2
if %opcao% equ 3 goto opcao3
if %opcao% equ 0 goto opcao0
if %opcao% GEQ 6 goto opcao6

:opcao1
cls
docker-compose down
docker-compose rm
docker-compose pull
docker-compose build --no-cache
docker-compose up -d --force-recreate

echo ==================================
echo *      Docker criado!            *
echo ==================================
pause
goto menu

:opcao2
cls
echo Digite o nome do arquivo migrations 
SET /P input=
dotnet ef migrations add %input% --project src\Vanilla.Infra.Data --startup-project src\Vanilla.API
echo ==============================================
echo * Migrations  %input% concluido              *
echo ==============================================
pause
goto menu

:opcao3
cls
dotnet ef database update --project src\Vanilla.Infra.Data --startup-project src\Vanilla.API
echo ==============================================
echo * Migrations  %input% concluido              *
echo ==============================================
pause
goto menu

:opcao0
cls
exit

:opcao6
echo ==============================================
echo * Opcao Invalida! Escolha outra opcao do menu *
echo ==============================================
pause
goto menu