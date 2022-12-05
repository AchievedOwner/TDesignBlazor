﻿using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

using System.Text.Json;

namespace TDesign
{

    /// <summary>
    /// 锚点
    /// </summary>
    [HtmlTag("div")]
    [ParentComponent]
    [CssClass("t-anchor")]
    public class TAnchor : BlazorComponentBase, IHasChildContent, IHasOnSwitch
    {

        private DotNetObjectReference<TAnchor>? objRef;

        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获取或设置锚点关联滚动容器
        /// </summary>
        [Parameter][HtmlAttribute] public string? Container { get; set; }

        [Inject] public new IJSRuntime? JS { get; set; }

        public EventCallback<int?> OnSwitch { get; set; }

        /// <summary>
        /// 获取或设置切换索引
        /// </summary>
        public int? SwitchIndex { get; set; } = 0;

        public void Dispose()
        {
            objRef?.Dispose();
        }

        /// <summary>
        /// 监听锚点关联容器滚动位置
        /// </summary>
        /// <param name="index">scrollTop 位置</param>
        [JSInvokable]
        public async Task OnScrollAnchorChangeAsync(int index)
        {
            var containerId = Container?.Split("#")[1];
            index += await JS!.InvokeAsync<int>("getOffsetTop", containerId);
            for (int i = 0; i < ChildComponents.Count; i++)
            {
                if (ChildComponents[i] is TAnchorItem item)
                {
                    var itemId = item.Href?.Split("#")[1];
                    item.OffsetTop = await JS!.InvokeAsync<int>("getOffsetTop", itemId);
                    item.OffsetHeight = await JS!.InvokeAsync<int>("getOffsetHeight", itemId);

                    var start = item.OffsetTop;
                    var end = item.OffsetTop+ item.OffsetHeight;

                    if (start <= index && index <= end)
                    {
                        SwitchIndex = item.Index;
                        item.SetActive(true);
                    }
                    else
                    {
                        item.SetActive(false);
                    }


                }
            }
            await this.Refresh();
        }

        protected override void AddContent(RenderTreeBuilder builder, int sequence)
        {
            BuildLine(builder, sequence + 1);
            builder.AddContent(sequence + 2, ChildContent);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!string.IsNullOrEmpty(Container))
            {
                var containerId = Container?.Split("#")[1];
                JS?.InvokeVoidAsync("onAnchorScroll", objRef, containerId);
            }

            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            objRef = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// 生成侧标竖线
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="builder"></param>
        private void BuildLine(RenderTreeBuilder builder, int sequence)
        {

            builder.CreateElement(sequence + 1, "div", line =>
            {

                line.CreateElement(sequence + 2, "div", wrapper =>
                {

                    wrapper.CreateElement(sequence + 3, "div", attributes: new
                    {

                        @class = "t-anchor__line-cursor",
                    });

                }, new { @class = "t-anchor__line-cursor-wrapper", style = $"top: {SwitchIndex * 24}px; height: 24px; opacity: 1;" });

            }, new { @class = "t-anchor__line" });

        }
    }
}