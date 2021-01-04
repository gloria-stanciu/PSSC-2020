using GrainInterfaces;
using Orleans.Streams;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    class QuestionProjectionGrain : Orleans.Grain, IQuestionProjectionGrain, IAsyncObserver<Post>
    {
        private readonly StackUnderflowContext _stackUnderflowContext;
        private IList<Post> _questions;
        private readonly int _tenantId = 1;
        public QuestionProjectionGrain(StackUnderflowContext stackUnderflowContext = null)
        {
            _stackUnderflowContext = stackUnderflowContext;
        }
        public async Task<IEnumerable<Post>> GetQuestionsAsync()
        {
            return _questions;
        }
        public async Task<IEnumerable<Post>> GetQuestionAsync(int questionId)
        {
            return _questions.Where(p => p.PostId == questionId);
        }
        public override async Task OnActivateAsync()
        {
            IAsyncStream<Post> stream = this.GetStreamProvider("SMSProvider").GetStream<Post>(Guid.Empty, "questions");
            await stream.SubscribeAsync(this);
            _questions = new List<Post>() {
                new Post
                {
                    PostId = 1,
                    PostText ="My question"
                }
            };
        }
        public async Task OnNextAsync(Post item, StreamSequenceToken token = null)
        {
            _questions.Add(item);
        }
        public Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
