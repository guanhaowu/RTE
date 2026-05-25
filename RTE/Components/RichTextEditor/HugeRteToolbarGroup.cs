using RTE.Components.RichTextEditor.Enum;

namespace RTE.Components.RichTextEditor;

public sealed class HugeRteToolbarGroup
{
    public IReadOnlyList<HugeRteToolbarItem> Items { get; set; } = [];
}