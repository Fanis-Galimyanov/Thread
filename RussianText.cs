using System.Text;

internal static class RussianText
{
   internal static void connectRussianText()
   {
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      var enc1251 = Encoding.GetEncoding(1251);
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;
      System.Console.InputEncoding = enc1251;
   }
}

