using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace ClipboardCharCountNotify
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var notify = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true
            };

            try
            {
                if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText();
                    if (!string.IsNullOrEmpty(text))
                    {
                        int charCount = new StringInfo(text).LengthInTextElements;
                        notify.ShowBalloonTip(3000, "", $"クリップボード文字数: {charCount}", ToolTipIcon.Info);
                    }
                    else
                    {
                        notify.ShowBalloonTip(3000, "", "テキストデータがありません", ToolTipIcon.Info);
                    }
                }
                else
                {
                    notify.ShowBalloonTip(3000, "", "テキストデータがありません", ToolTipIcon.Info);
                }
            }
            catch (Exception ex)
            {
                notify.ShowBalloonTip(3000, "", $"エラー: {ex.Message}", ToolTipIcon.Error);
            }
            finally
            {
                System.Threading.Thread.Sleep(3000); // 通知表示時間
                notify.Visible = false;
                notify.Dispose();
            }
        }
    }
}