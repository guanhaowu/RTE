using RTE.Components.RichTextEditor.Enum;
using RTE.Components.RichTextEditor.Extensions;

namespace RTE.Components.RichTextEditor;

public static class RichTextEditorOptionsMapper
{
    public static object ToHugeRteConfig(RichTextEditorOptions options)
    {
        return new Dictionary<string, object?>
            {
                ["height"] = options.Height,
                ["min_height"] = options.MinHeight,
                ["max_height"] = options.MaxHeight,
                ["width"] = options.Width,
                ["menubar"] = options.Menubar,
                ["statusbar"] = options.Statusbar,
                ["branding"] = options.Branding,
                ["resize"] = options.Resize,
                ["browser_spellcheck"] = options.BrowserSpellcheck,
                ["promotion"] = options.Promotion,
                ["plugins"] = ToPluginString(GetPlugins(options)),
                ["toolbar"] = ToToolbarString(options.Toolbar),
                ["block_formats"] = string.Join("; ", options.BlockFormats.Select(ToHugeRteValue)),
                ["fontsize_formats"] = string.Join(" ", options.FontSizeFormats.Select(x => x.Value)),
                ["language"] = options.Language,
                ["language_url"] = options.LanguageUrl,
                ["placeholder"] = options.Placeholder,
                ["paste_data_images"] = options.PasteDataImages,
                ["automatic_uploads"] = options.AutomaticUploads,
                ["content_style"] = options.ContentStyle,
                ["directionality"] = options.Directionality,
                ["quickbars_selection_toolbar"] = options.SelectionQuickbar ? GetQuickbarSelectionToolbar(options) : false,
                ["quickbars_insert_toolbar"] = options.InsertQuickbar ? options.QuickbarInsertToolbar : false,
                ["quickbars_image_toolbar"] = options.ImageQuickbar ? options.QuickbarImageToolbar : false,
            }
            .Where(pair => pair.Value is not null)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    private static HugeRtePlugins GetPlugins(RichTextEditorOptions options)
    {
        var plugins =
            options.ExtraPlugins |
            GetPluginsRequiredByOptions(options) |
            GetPluginsRequiredByToolbar(options.Toolbar);

        return AddPluginDependencies(plugins);
    }

    private static string GetQuickbarSelectionToolbar(RichTextEditorOptions options)
    {
        var toolbar = options.QuickbarSelectionToolbar;

        if (GetPlugins(options).HasFlagFast(HugeRtePlugins.Link))
            return toolbar;

        return string.Join(" ",
            toolbar
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(item => !item.Equals("quicklink", StringComparison.OrdinalIgnoreCase)));
    }

    private static HugeRtePlugins GetPluginsRequiredByOptions(RichTextEditorOptions options)
    {
        return options.SelectionQuickbar || options.InsertQuickbar || options.ImageQuickbar
            ? HugeRtePlugins.Quickbars
            : HugeRtePlugins.None;
    }

    private static HugeRtePlugins AddPluginDependencies(HugeRtePlugins plugins)
    {
        if (plugins.HasFlagFast(HugeRtePlugins.AdvancedList))
            plugins |= HugeRtePlugins.Lists;

        return plugins;
    }

    private static HugeRtePlugins GetPluginsRequiredByToolbar(IReadOnlyList<HugeRteToolbarGroup> toolbar)
    {
        var plugins = HugeRtePlugins.None;

        foreach (var item in toolbar.SelectMany(group => group.Items))
        {
            plugins |= item switch
            {
                HugeRteToolbarItem.Anchor => HugeRtePlugins.Anchor,
                HugeRteToolbarItem.BulletList => HugeRtePlugins.Lists,
                HugeRteToolbarItem.NumberedList => HugeRtePlugins.Lists,
                HugeRteToolbarItem.Code => HugeRtePlugins.Code,
                HugeRteToolbarItem.CodeSample => HugeRtePlugins.CodeSample,
                HugeRteToolbarItem.Fullscreen => HugeRtePlugins.Fullscreen,
                HugeRteToolbarItem.Help => HugeRtePlugins.Help,
                HugeRteToolbarItem.Image => HugeRtePlugins.Image,
                HugeRteToolbarItem.Link => HugeRtePlugins.Link,
                HugeRteToolbarItem.Unlink => HugeRtePlugins.Link,
                HugeRteToolbarItem.Media => HugeRtePlugins.Media,
                HugeRteToolbarItem.Preview => HugeRtePlugins.Preview,
                HugeRteToolbarItem.SearchReplace => HugeRtePlugins.SearchReplace,
                HugeRteToolbarItem.Table => HugeRtePlugins.Table,
                HugeRteToolbarItem.VisualBlocks => HugeRtePlugins.VisualBlocks,
                HugeRteToolbarItem.VisualChars => HugeRtePlugins.VisualChars,
                HugeRteToolbarItem.WordCount => HugeRtePlugins.WordCount,
                HugeRteToolbarItem.LeftToRight => HugeRtePlugins.Directionality,
                HugeRteToolbarItem.RightToLeft => HugeRtePlugins.Directionality,
                _ => HugeRtePlugins.None
            };
        }

        return plugins;
    }

    private static string ToPluginString(HugeRtePlugins plugins)
    {
        HugeRtePlugins[] order =
        [
            HugeRtePlugins.Accordion,
            HugeRtePlugins.AdvancedList,
            HugeRtePlugins.Anchor,
            HugeRtePlugins.AutoLink,
            HugeRtePlugins.AutoResize,
            HugeRtePlugins.AutoSave,
            HugeRtePlugins.CharacterMap,
            HugeRtePlugins.Code,
            HugeRtePlugins.CodeSample,
            HugeRtePlugins.Directionality,
            HugeRtePlugins.Emoticons,
            HugeRtePlugins.Fullscreen,
            HugeRtePlugins.Help,
            HugeRtePlugins.Image,
            HugeRtePlugins.ImportCss,
            HugeRtePlugins.InsertDateTime,
            HugeRtePlugins.Link,
            HugeRtePlugins.Lists,
            HugeRtePlugins.Media,
            HugeRtePlugins.NonBreaking,
            HugeRtePlugins.PageBreak,
            HugeRtePlugins.Preview,
            HugeRtePlugins.Quickbars,
            HugeRtePlugins.Save,
            HugeRtePlugins.SearchReplace,
            HugeRtePlugins.Table,
            HugeRtePlugins.Template,
            HugeRtePlugins.VisualBlocks,
            HugeRtePlugins.VisualChars,
            HugeRtePlugins.WordCount
        ];

        return string.Join(" ", order.Where(plugin => plugins.HasFlagFast(plugin)).Select(ToHugeRteValue));
    }

    private static string ToToolbarString(IReadOnlyList<HugeRteToolbarGroup> toolbar)
    {
        return string.Join(" | ",
            toolbar
                .Select(group => string.Join(" ", group.Items.Select(ToHugeRteValue)))
                .Where(group => !string.IsNullOrWhiteSpace(group)));
    }

    private static string ToHugeRteValue(HugeRtePlugins plugins)
    {
        return plugins switch
        {
            HugeRtePlugins.Accordion => "accordion",
            HugeRtePlugins.AdvancedList => "advlist",
            HugeRtePlugins.Anchor => "anchor",
            HugeRtePlugins.AutoLink => "autolink",
            HugeRtePlugins.AutoResize => "autoresize",
            HugeRtePlugins.AutoSave => "autosave",
            HugeRtePlugins.CharacterMap => "charmap",
            HugeRtePlugins.Code => "code",
            HugeRtePlugins.CodeSample => "codesample",
            HugeRtePlugins.Directionality => "directionality",
            HugeRtePlugins.Emoticons => "emoticons",
            HugeRtePlugins.Fullscreen => "fullscreen",
            HugeRtePlugins.Help => "help",
            HugeRtePlugins.Image => "image",
            HugeRtePlugins.ImportCss => "importcss",
            HugeRtePlugins.InsertDateTime => "insertdatetime",
            HugeRtePlugins.Link => "link",
            HugeRtePlugins.Lists => "lists",
            HugeRtePlugins.Media => "media",
            HugeRtePlugins.NonBreaking => "nonbreaking",
            HugeRtePlugins.PageBreak => "pagebreak",
            HugeRtePlugins.Preview => "preview",
            HugeRtePlugins.Quickbars => "quickbars",
            HugeRtePlugins.Save => "save",
            HugeRtePlugins.SearchReplace => "searchreplace",
            HugeRtePlugins.Table => "table",
            HugeRtePlugins.Template => "template",
            HugeRtePlugins.VisualBlocks => "visualblocks",
            HugeRtePlugins.VisualChars => "visualchars",
            HugeRtePlugins.WordCount => "wordcount",
            _ => throw new ArgumentOutOfRangeException(nameof(plugins), plugins, "Unsupported plugin value")
        };
    }

    private static string ToHugeRteValue(HugeRteToolbarItem item)
    {
        return item switch
        {
            HugeRteToolbarItem.Undo => "undo",
            HugeRteToolbarItem.Redo => "redo",
            HugeRteToolbarItem.Blocks => "blocks",
            HugeRteToolbarItem.Bold => "bold",
            HugeRteToolbarItem.Italic => "italic",
            HugeRteToolbarItem.Underline => "underline",
            HugeRteToolbarItem.StrikeThrough => "strikethrough",
            HugeRteToolbarItem.BlockQuote => "blockquote",
            HugeRteToolbarItem.BulletList => "bullist",
            HugeRteToolbarItem.NumberedList => "numlist",
            HugeRteToolbarItem.Outdent => "outdent",
            HugeRteToolbarItem.Indent => "indent",
            HugeRteToolbarItem.Link => "link",
            HugeRteToolbarItem.Unlink => "unlink",
            HugeRteToolbarItem.Anchor => "anchor",
            HugeRteToolbarItem.Image => "image",
            HugeRteToolbarItem.Media => "media",
            HugeRteToolbarItem.Table => "table",
            HugeRteToolbarItem.ForeColor => "forecolor",
            HugeRteToolbarItem.BackColor => "backcolor",
            HugeRteToolbarItem.RemoveFormat => "removeformat",
            HugeRteToolbarItem.AlignLeft => "alignleft",
            HugeRteToolbarItem.AlignCenter => "aligncenter",
            HugeRteToolbarItem.AlignRight => "alignright",
            HugeRteToolbarItem.AlignJustify => "alignjustify",
            HugeRteToolbarItem.Code => "code",
            HugeRteToolbarItem.CodeSample => "codesample",
            HugeRteToolbarItem.Fullscreen => "fullscreen",
            HugeRteToolbarItem.Preview => "preview",
            HugeRteToolbarItem.SearchReplace => "searchreplace",
            HugeRteToolbarItem.VisualBlocks => "visualblocks",
            HugeRteToolbarItem.VisualChars => "visualchars",
            HugeRteToolbarItem.WordCount => "wordcount",
            HugeRteToolbarItem.Help => "help",
            HugeRteToolbarItem.LeftToRight => "ltr",
            HugeRteToolbarItem.RightToLeft => "rtl",
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, "Unsupported toolbar item value")
        };
    }

    private static string ToHugeRteValue(HugeRteBlockFormatOption option)
    {
        return $"{option.Label}={ToHugeRteValue(option.Format)}";
    }

    private static string ToHugeRteValue(HugeRteFormats format)
    {
        return format switch
        {
            HugeRteFormats.Paragraph => "p",
            HugeRteFormats.Heading1 => "h1",
            HugeRteFormats.Heading2 => "h2",
            HugeRteFormats.Heading3 => "h3",
            HugeRteFormats.Heading4 => "h4",
            HugeRteFormats.Heading5 => "h5",
            HugeRteFormats.Heading6 => "h6",
            HugeRteFormats.Preformatted => "pre",
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, "Unsupported format value")
        };
    }

}