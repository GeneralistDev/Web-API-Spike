using FluentValidation;
using FluentValidation.Attributes;
using MediatR;

namespace Web_API_Spike.Commands
{
    [Validator(typeof(RequestValidator))]
    public class TestCommandRequest : IRequest<string>
    {
        public string Message;

        public class RequestValidator : AbstractValidator<TestCommandRequest>
        {
            public RequestValidator()
            {
                RuleFor(r => r.Message).NotEmpty().WithMessage("message cannot be empty");
            }
        }
    }

    public class TestCommand: IRequestHandler<TestCommandRequest, string>
    {
        public string Handle(TestCommandRequest message)
        {
            return message.Message;
        }
    }
}