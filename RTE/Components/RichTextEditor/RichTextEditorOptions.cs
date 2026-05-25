using RTE.Components.RichTextEditor.Enum;

namespace RTE.Components.RichTextEditor;

public sealed class RichTextEditorOptions
{
    public int Height { get; set; } = 500;
    public int MinHeight { get; set; } = 240;
    public int? MaxHeight { get; set; }
    public string Width { get; set; } = "100%";

    public bool Menubar { get; set; } = false;
    public bool Statusbar { get; set; }
    public bool Branding { get; set; } = false;
    public bool Resize { get; set; } = true;
    public bool BrowserSpellcheck { get; set; } = true;
    public bool Promotion { get; set; } = false;

    public string Placeholder { get; set; } = "Start typing...";
    public string Language { get; set; } = "en";
    public string? LanguageUrl { get; set; }
    public string Directionality { get; set; } = "ltr";

    public bool PasteDataImages { get; set; } = false;
    public bool AutomaticUploads { get; set; } = false;
    public bool SelectionQuickbar { get; set; } = true;
    public bool InsertQuickbar { get; set; }
    public bool ImageQuickbar { get; set; }
    public string QuickbarSelectionToolbar { get; set; } = "bold italic underline | quicklink h1 h2 h3";
    public string QuickbarInsertToolbar { get; set; } = "quickimage quicktable";
    public string QuickbarImageToolbar { get; set; } = "alignleft aligncenter alignright | rotateleft rotateright | imageoptions";

    public HugeRtePlugins ExtraPlugins { get; set; } =
        HugeRtePlugins.AdvancedList
        | HugeRtePlugins.WordCount
        ;

    public IReadOnlyList<HugeRteToolbarGroup> Toolbar { get; set; } =
    [
        new() { Items = [HugeRteToolbarItem.Undo, HugeRteToolbarItem.Redo] },
        new() { Items = [HugeRteToolbarItem.Blocks] },
        new() { Items = [HugeRteToolbarItem.Bold, HugeRteToolbarItem.Italic, HugeRteToolbarItem.Underline] },
        new() { Items = [HugeRteToolbarItem.BulletList, HugeRteToolbarItem.NumberedList] },
        new() { Items = [HugeRteToolbarItem.AlignLeft, HugeRteToolbarItem.AlignCenter, HugeRteToolbarItem.AlignRight] },
        new() { Items = [HugeRteToolbarItem.Outdent, HugeRteToolbarItem.Indent] }
    ];

    public IReadOnlyList<HugeRteBlockFormatOption> BlockFormats { get; set; } =
    [
        new() { Label = "Paragraph", Format = HugeRteFormats.Paragraph },
        new() { Label = "Heading 1", Format = HugeRteFormats.Heading1 },
        new() { Label = "Heading 2", Format = HugeRteFormats.Heading2 },
        new() { Label = "Heading 3", Format = HugeRteFormats.Heading3 }
    ];

    public IReadOnlyList<HugeRteFontSizeOption> FontSizeFormats { get; set; } =
    [
        new() { Value = "10px" },
        new() { Value = "12px" },
        new() { Value = "14px" },
        new() { Value = "16px" },
        new() { Value = "18px" },
        new() { Value = "24px" },
        new() { Value = "36px" }
    ];

    public string ContentStyle { get; set; } = """
                                               body {
                                                   overflow-wrap: anywhere;
                                                   word-break: break-word;
                                               }
                                               """;
}