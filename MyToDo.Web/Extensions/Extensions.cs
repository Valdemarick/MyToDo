using MyToDo.Application.Common.Dtos.Common;

namespace MyToDo.Web.Extensions;

internal static class Extensions
{
    public static async Task<string> GetQueryFromRequestDto(this BasePageRequestDto dto)
    {
        var queryParameters = new Dictionary<string, string>();

        foreach (var property in dto.GetType().GetProperties())
        {
            if (property.GetValue(dto) is not null)
            {
                queryParameters.Add(property.Name, property.GetValue(dto).ToString());
            }
        }

        return await new FormUrlEncodedContent(queryParameters).ReadAsStringAsync();
    }
}
