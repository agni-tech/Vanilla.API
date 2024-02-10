using Vanilla.Domain.Common.Interfaces;
using Vanilla.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using MySaviors.Helpers.Extensions;
using System.Text;

namespace Vanilla.Domain.Common.Services;

public class FileService : IFileService
{
    //public List<MeteringDto> GetMeteringFile(IFormFile file, int startLine = 1)
    //{
    //    var lines = new List<MeteringDto>();

    //    using var uploadedFile = file.OpenReadStream();
    //    try
    //    {
    //        string line = null;
    //        var rd = new StreamReader(uploadedFile, Encoding.Default, true);
    //        while ((line = rd.ReadLine()) != null)
    //        {
    //            if (startLine == 1)
    //                lines.Add(MapMeteringFile(line));
    //            else
    //                startLine--;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        NotificationsWrapper.AddMessage(ex.Message);
    //        return null;
    //    }

    //    return lines;
    //}


    //private static MeteringDto MapMeteringFile(string currentLine)
    //{
    //    MeteringDto result = null;
    //    var line = currentLine.Split(';');

    //    if (currentLine.HaveAny())
    //    {
    //        result = new();
    //        result.MeteringPoint = line[0];
    //        result.SummaryDate = line[1].IsDate() ? DateTime.Parse(line[1]) : null;
    //        result.SummaryHour = line[2].IsNumeric() ? int.Parse(line[2]) : null;
    //        result.PowerType = line[3];
    //        result.ActiveGeneration = line[4].IsNumeric() ? float.Parse(line[4]) : null;
    //        result.ActiveConsume = line[5].IsNumeric() ? float.Parse(line[5]) : null;
    //        result.ReactiveGeneration = line[6].IsNumeric() ? float.Parse(line[6]) : null;
    //        result.ReactiveConsume = line[7].IsNumeric() ? float.Parse(line[7]) : null;
    //        result.TotalMissingIntervals = line[8].IsNumeric() ? float.Parse(line[8]) : null;
    //        result.MeteringSituation = line[9];
    //        result.ReasonSituation = line[10];
    //    }

    //    return result;
    //}
}
