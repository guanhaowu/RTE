namespace RTE.Components.RichTextEditor.Enum;

[Flags]
public enum HugeRtePlugins : ulong
{
    None = 0,

    Accordion = 1UL << 0,
    AdvancedList = 1UL << 1,
    Anchor = 1UL << 2,
    AutoLink = 1UL << 3,
    AutoResize = 1UL << 4,
    AutoSave = 1UL << 5,
    CharacterMap = 1UL << 6,
    Code = 1UL << 7,
    CodeSample = 1UL << 8,
    Directionality = 1UL << 9,
    Emoticons = 1UL << 10,
    Fullscreen = 1UL << 11,
    Help = 1UL << 12,
    Image = 1UL << 13,
    ImportCss = 1UL << 14,
    InsertDateTime = 1UL << 15,
    Link = 1UL << 16,
    Lists = 1UL << 17,
    Media = 1UL << 18,
    NonBreaking = 1UL << 19,
    PageBreak = 1UL << 20,
    Preview = 1UL << 21,
    Quickbars = 1UL << 22,
    Save = 1UL << 23,
    SearchReplace = 1UL << 24,
    Table = 1UL << 25,
    Template = 1UL << 26,
    VisualBlocks = 1UL << 27,
    VisualChars = 1UL << 28,
    WordCount = 1UL << 29,
}