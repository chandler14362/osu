// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osuTK;

namespace osu.Game.Screens.Select.FooterV2
{
    public partial class FooterV2 : CompositeDrawable
    {
        //Should be 60, setting to 50 for now for the sake of matching the current BackButton height.
        private const int height = 50;
        private const int padding = 80;

        private readonly List<OverlayContainer> overlays = new List<OverlayContainer>();

        public void AddButton(FooterButtonV2 button, OverlayContainer? overlay = null)
        {
            if (overlay != null)
            {
                overlays.Add(overlay);
                button.Action = () => showOverlay(overlay);
            }

            buttons.Add(button);
        }

        private void showOverlay(OverlayContainer overlay)
        {
            foreach (var o in overlays)
            {
                if (o == overlay)
                    o.ToggleVisibility();
                else
                    o.Hide();
            }
        }

        private FillFlowContainer<FooterButtonV2> buttons = null!;

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            RelativeSizeAxes = Axes.X;
            Height = height;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colour.B5
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Position = new Vector2(TwoLayerButton.SIZE_EXTENDED.X + padding, 40),
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(padding, 0),
                    Children = new Drawable[]
                    {
                        buttons = new FillFlowContainer<FooterButtonV2>
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(-FooterButtonV2.SHEAR_WIDTH + 7, 0),
                            AutoSizeAxes = Axes.Both
                        }
                    }
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;

        protected override bool OnClick(ClickEvent e) => true;

        protected override bool OnHover(HoverEvent e) => true;
    }
}
