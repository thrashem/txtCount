using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Linq;

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
                        // 改行をカウントせず、空白はカウントする
                        int charCount = text
                            .Where(c => c != '\r' && c != '\n') // 改行文字（CR, LF）を除外
                            .Count(c => !char.IsControl(c) && c != '\u200B'); // 制御文字とゼロ幅スペースを除外
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
                System.Threading.Thread.Sleep(3000);
                notify.Visible = false;
                notify.Dispose();
            }
        }
    }
}