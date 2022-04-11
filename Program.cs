using System.Text;

// Подключаем русский язык------------------------------------------------//
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var enc1251 = Encoding.GetEncoding(1251);
System.Console.OutputEncoding = System.Text.Encoding.UTF8;
System.Console.InputEncoding = enc1251;
//------------------------------------------------------------------------//

string st1="";
string st2="";
string guid_string;
string[] st_array = new string[100];
string[] st_array_for_method = new string[100];
DummyRequestHandler dummyRequest = new DummyRequestHandler();
int count, strung_count = 0;

Console.WriteLine("Приложение запущено.");
do
{
    Console.WriteLine("Введите текст запроса для отправки. Для выхода введите /exit");
    Console.ForegroundColor = ConsoleColor.Red;
    st1 = Console.ReadLine();
    Console.ResetColor();
    if (!st1.Contains("/exit"))
    {
        Console.WriteLine($"Будет послано сообщение '{st1}'");
        Console.WriteLine("Введите аргумент сообщения. Если аргумента нет введите /end");
        Console.ForegroundColor = ConsoleColor.Red;
        st2 = Console.ReadLine();
        Console.ResetColor();
        count = 0;
        st_array[count] = st2;
        while (!st2.Contains("/end"))
        {
            count++;
            Console.WriteLine("Введите следующий аргумент сообщения. Для окончания добавления аргументов введите /end");
            Console.ForegroundColor = ConsoleColor.Red;
            st2 = Console.ReadLine();
            Console.ResetColor();
            st_array[count] = st2;
        }
        st_array_for_method[strung_count] = st1;
        void method() 
        {
            try
            {
                guid_string = dummyRequest.HandleRequest(st_array_for_method[strung_count], st_array);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Было отправлено сообщение '{st_array_for_method[strung_count]}'. Присвоет идентификатор {guid_string}");
                Console.ResetColor();
                strung_count++;
                ThreadPool.QueueUserWorkItem(callBack => answer(guid_string));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Что то упало! {ex.Message}");
            }
        }
        ThreadPool.QueueUserWorkItem(callBack => method());
        
    }
} 
while (!st1.Contains("/exit"));



Console.WriteLine("Приложение завершает работу.");

static void answer(string guid_string)
{
    Thread.Sleep(10_000);
    Console.WriteLine($"Сообщение с идентификатором {guid_string} получило ответ {Guid.NewGuid().ToString("D")}");
}

public interface IRequestHandler
{
    string HandleRequest(string message, string[] arguments);
}
public class DummyRequestHandler : IRequestHandler
{
    public string HandleRequest(string message, string[] arguments)
    {
        // Притворяемся, что делаем что то.
        Thread.Sleep(10_000);
        if (message.Contains("упади"))
        {
            throw new Exception("Я упал, как сам просил");
        }
        return Guid.NewGuid().ToString("D");
    }
}