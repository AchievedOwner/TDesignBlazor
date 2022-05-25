﻿using Markdig;

using Microsoft.AspNetCore.Components;

namespace AnyDesignBlazor.Components.TDesign.Docs.Shared;
public static class Code
{
    public static MarkupString Create(string value)
    {
        var content = Markdown.ToHtml(value, new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseEmojiAndSmiley()
            .UseSyntaxHighlighting()
            .Build());
        return new MarkupString(content);
    }
}
