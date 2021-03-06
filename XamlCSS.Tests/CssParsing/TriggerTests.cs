﻿using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlCSS.CssParsing;

namespace XamlCSS.Tests.CssParsing
{
    [TestFixture]
    public class TriggerTests
    {
        [Test]
        public void PropertyTrigger_should_be_added_to_Triggers()
        {
            var content = @"
Button
{
    @Property IsFocussed True
    {
        BackgroundColor: Red;
        ForegroundColor: Green;
    }
}
";
            var styleSheet = CssParser.Parse(content);

            var first = styleSheet.Rules[0].DeclarationBlock.Triggers[0] as Trigger;
            first.Property.Should().Be("IsFocussed");
            first.Value.Should().Be("True");

            first.StyleDeclaraionBlock[0].Property.Should().Be("BackgroundColor");
            first.StyleDeclaraionBlock[0].Value.Should().Be("Red");
            first.StyleDeclaraionBlock[1].Property.Should().Be("ForegroundColor");
            first.StyleDeclaraionBlock[1].Value.Should().Be("Green");
        }

        [Test]
        public void PropertyTrigger_with_quoted_value_should_be_added_to_Triggers()
        {
            var content = @"
Button
{
    @Property Text ""SomeValue""
    {
        BackgroundColor: Red;
    }
}
";
            var styleSheet = CssParser.Parse(content);

            var first = styleSheet.Rules[0].DeclarationBlock.Triggers[0] as Trigger;
            first.Property.Should().Be("Text");
            first.Value.Should().Be("SomeValue");

            first.StyleDeclaraionBlock[0].Property.Should().Be("BackgroundColor");
            first.StyleDeclaraionBlock[0].Value.Should().Be("Red");
        }

        [Test]
        public void DataTrigger_should_be_added_to_Triggers()
        {
            var content = @"
Button
{
    @Data Text.Length 10
    {
        IsEnabled: False;
        ForegroundColor: Red;
    }
}
";
            var styleSheet = CssParser.Parse(content);
            var first = styleSheet.Rules[0].DeclarationBlock.Triggers[0] as DataTrigger;

            first.Binding.Should().Be("Text.Length");
            first.Value.Should().Be("10");
        }

        [Test]
        public void EventTrigger_should_be_added_to_Triggers()
        {
            var content = @"
Button
{
    @Event Clicked
    {
        BeginStoryboard: #StaticResource fadeOutAndInStoryboard;
        Transition: FontSize initial 50 500ms ease-in-out, Width 100 200 500ms, Height [ initial 300 200ms, initial 200 500ms];
    }
}
";
            var styleSheet = CssParser.Parse(content);
            var first = styleSheet.Rules[0].DeclarationBlock.Triggers[0] as EventTrigger;

            first.Event.Should().Be("Clicked");

            first.Actions[0].Action.Should().Be("BeginStoryboard");
            first.Actions[0].Parameters.Should().Be("#StaticResource fadeOutAndInStoryboard");

            first.Actions[1].Action.Should().Be("Transition");
            first.Actions[1].Parameters.Should().Be("FontSize initial 50 500ms ease-in-out, Width 100 200 500ms, Height [ initial 300 200ms, initial 200 500ms]");
        }

        [Test]
        [Ignore]
        public void Animations()
        {
            var content = @"
@keyframes fade-out-and-in
{
    0% { Opacity: 1; }
    50% { Opacity: 0; }
    100% { Opacity: 1; }
}

Button
{
    @EventTrigger Clicked
    {
        Animation: fade-out-and-in 5s;
    }
}
";
            var styleSheet = CssParser.Parse(content);
            var first = styleSheet.Rules[0].DeclarationBlock.Triggers[0] as EventTrigger;

            first.Event.Should().Be("Clicked");
        }
    }
}
