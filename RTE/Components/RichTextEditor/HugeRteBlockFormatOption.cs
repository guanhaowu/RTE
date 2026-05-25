using RTE.Components.RichTextEditor.Enum;

namespace RTE.Components.RichTextEditor;

public sealed class HugeRteBlockFormatOption
{
    public required string Label { get; init; }
    public required HugeRteFormats Format { get; init; }
}