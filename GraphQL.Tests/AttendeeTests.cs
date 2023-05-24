using Backend.Common.Data;
using GraphQL.Common.Types;
using GraphQL.Common.Types.Mutations;
using GraphQL.Common.Types.Queries;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace GraphQL.Tests
{
    public class AttendeeTests
    {
        private IRequestExecutorBuilder GetRequestExecutorBuilder()
        {
            return new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("Data Source=conferences.db"))
                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<AttendeeMutations>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                .AddGlobalObjectIdentification();
        }

        [Fact]
        public async Task Attendee_Schema_Changed()
        {
            ISchema schema = await GetRequestExecutorBuilder().BuildSchemaAsync();

            // assert
            schema.Print().MatchSnapshot();
        }

        [Fact]
        public async Task RegisterAttendee()
        {
            // arrange
            IRequestExecutor executor = await GetRequestExecutorBuilder().BuildRequestExecutorAsync();

            // act
            IExecutionResult result = await executor.ExecuteAsync(@"
            mutation RegisterAttendee {
                registerAttendee(
                    input: {
                        emailAddress: ""michael@chillicream.com""
                            firstName: ""michael""
                            lastName: ""staib""
                            userName: ""michael3""
                        })
                {
                    attendee {
                        id
                    }
                }
            }");

            // assert
            result.ToJson().MatchSnapshot();
        }
    }
}