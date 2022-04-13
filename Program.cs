RussianText.connectRussianText();
DummyRequestHandler dummyRequest = new DummyRequestHandler();
List<string> responsesReceivedFromTheRecipient = new List<string>();

string requestText="", messageArgument="", guidString;
string[] arrayOfMessageArguments = new string[100];
int numberOfMessageArguments;

Console.WriteLine("Приложение запущено.");
do
{
    Console.WriteLine("Введите текст запроса для отправки. Для выхода введите /exit");
    redInputInConsole(ref requestText);
    if (!requestText.Contains("/exit"))
    {
        Console.WriteLine($"Будет послано сообщение '{requestText}'");
        Console.WriteLine("Введите аргумент сообщения. Если аргумента нет введите /end");
        redInputInConsole(ref messageArgument);
        numberOfMessageArguments = 0;
        arrayOfMessageArguments[numberOfMessageArguments] = messageArgument;
        while (!messageArgument.Contains("/end"))
        {
            numberOfMessageArguments++;
            Console.WriteLine("Введите следующий аргумент сообщения. Для окончания добавления аргументов введите /end");
            redInputInConsole(ref messageArgument);
            arrayOfMessageArguments[numberOfMessageArguments] = messageArgument;
        }
        void flowUnit(string requestText) 
        {
            try
            {
                guidString = dummyRequest.HandleRequest(requestText, arrayOfMessageArguments);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Было отправлено сообщение '{requestText}'. Присвоет идентификатор {guidString}");
                Console.ResetColor();
                responsesReceivedFromTheRecipient.Add("Сообщение с идентификатором " + guidString + " получило ответ " + Guid.NewGuid().ToString("D"));
            }
            catch (Exception ex)
            {
                guidString = Guid.NewGuid().ToString("D");
                Console.WriteLine($"Было отправлено сообщение '{requestText}'. Присвоет идентификатор {guidString}");
                responsesReceivedFromTheRecipient.Add("Сообщение с идентификатором " + guidString + " упало с ошибкой: " + ex.Message);
                responsesFromTheRecipient(responsesReceivedFromTheRecipient);
            }
        }
        ThreadPool.QueueUserWorkItem(callBack => flowUnit(requestText));
    }
} 
while (!requestText.Contains("/exit"));
Console.WriteLine("Приложение завершает работу.");

void responsesFromTheRecipient(List<string> responsesReceivedFromTheRecipient)
{
    foreach (string answerString in responsesReceivedFromTheRecipient)
    {
        Console.WriteLine(answerString);
    }
    responsesReceivedFromTheRecipient.Clear();
}
void redInputInConsole(ref string stringVariable)
{
    Console.ForegroundColor = ConsoleColor.Red;
    stringVariable = Console.ReadLine();
    Console.ResetColor();
}