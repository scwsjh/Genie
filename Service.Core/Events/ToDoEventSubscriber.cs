using Furion.EventBus;

namespace Service.Core
{
    public class ToDoEventSubscriber : IEventSubscriber
    {
        public ToDoEventSubscriber()
        {
        }

        //[EventSubscribe(ToDoEvenEnum.EvenName.Task_Offer, NumRetries = 3, RetryTimeout = 3000)]
        //public async Task TaskOffer(EventHandlerExecutingContext context)
        //{
        //    var todo = context.Source;
        //    FightTaskOffer users = todo.Payload as FightTaskOffer;
        //    string userId = users.userId;
        //    string opUser = users.opUser;
        //    var offerService = App.GetService<IGameTaskOfferService>();
        //    await offerService.TaskOfferOver(userId, opUser);
        //    await Task.CompletedTask;
        //}
    }
}