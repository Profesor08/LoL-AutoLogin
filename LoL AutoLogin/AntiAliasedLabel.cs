using System.Drawing.Text;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    class AntiAliasedLabel : Label
    {
        public TextRenderingHint TextRenderingHint = TextRenderingHint.AntiAlias;

        public AntiAliasedLabel()
        {
            AutoSize = true;
            UseCompatibleTextRendering = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.TextRenderingHint = TextRenderingHint;
            base.OnPaint(e);
        }
    }
}
