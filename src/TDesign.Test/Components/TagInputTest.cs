using ComponentBuilder;

namespace TDesign.Test.Components;
public class TagInputTest : TestBase<TTagInput>
{
    [Fact(DisplayName = "TagInput - 渲染组件")]
    public void Test_Render()
    {
        GetComponent()
            .MarkupMatches(builder =>
            {
                builder.Div().Class("t-input__wrap t-tage-input").Content(content =>
                {
                    content.Div().Class("t-input t-size-m t-is-default t-input --prefix")
                            .Content(input =>
                            {
                                input.Div().Class("t-input__prefix").Close();
                                input.Open("input").Class("t-input__iner").Attributes(("type", "text")).Close();
                                input.Span().Class("t-input__input-pre").Close();
                            })
                            .Close();
                }).Close();
            });
    }
}
