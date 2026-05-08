using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace RuoYi.Common.Data;

/// <summary>
/// Entity基类
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// 搜索值
    /// </summary>
    [JsonIgnore]
    public string? SearchValue { get; set; }

    /// <summary>
    /// 创建部门
    /// </summary>
    public long? CreateDept { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public long? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public long? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Dictionary<string, object>? Params { get; set; } = new();
}
