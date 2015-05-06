using System;
using System.Drawing;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace Inferno
{
    public class UITest : InfernoScript
    {
        protected override int TickInterval
        {
            get { return 15; }
        }

        protected override void Setup()
        {
            try
            {
                var ui = new InfernoUIManager();
                ui.AddUIElement(new InfernoUIRow("Selectable", Color.Black, Color.White, 0, true, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("Selectable", Color.Black, Color.White, 0, true, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("NotSelectable", Color.Black, Color.Azure, 0, false, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("NotSelectable", Color.Black, Color.Azure, 0, false, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("Selectable", Color.Black, Color.White, 0, true, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("Selectable", Color.Black, Color.White, 0, true, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("NotSelectable", Color.Black, Color.Azure, 0, false, () => { ; }));
                ui.AddUIElement(new InfernoUIRow("NotSelectable", Color.Black, Color.Azure, 0, false, () => { ; }));

                InfernoCore.OnKeyDownAsObservable
                    .Where(x => x.KeyCode == Keys.W)
                    .Subscribe(_ => ui.MoveUp());

                InfernoCore.OnKeyDownAsObservable
                    .Where(x => x.KeyCode == Keys.S)
                    .Subscribe(_ => ui.MoveDown());

                OnTickAsObservable.Subscribe(_ => ui.Draw());
            }
            catch (Exception e)
            {
                LogWrite(e.ToString());
            }
        }
    }
}
