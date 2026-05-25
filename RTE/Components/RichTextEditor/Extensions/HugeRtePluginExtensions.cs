using RTE.Components.RichTextEditor.Enum;

namespace RTE.Components.RichTextEditor.Extensions;

public static class HugeRtePluginExtensions
{
    public static bool HasFlagFast(this HugeRtePlugins value, HugeRtePlugins flag)
    {
        return (value & flag) != 0;
    }
}