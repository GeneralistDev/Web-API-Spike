using MediatR;

namespace Web_API_Spike.Commands
{
    public class TestCommandRequest : IRequest<string>
    {
        public string Message;
    }

    public class TestCommand: IRequestHandler<TestCommandRequest, string>
    {
        public string Handle(TestCommandRequest message)
        {
            return message.Message;
        }
    }
}