using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Interfaces.Repositories.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrustructure.Data.Mongo.Repositories
{
    public class ProjectsRepository : MongoRepository<Project>, IProjectsRepository
    {
        public ProjectsRepository(string connectionString, string databaseName, string collectionName)
    : base(connectionString, databaseName, collectionName)
        {
        }
        public async Task<bool> DeleteProject(int userId, string projectName)
        {
            var userIdFilter = Builders<Project>.Filter.Eq("UserId", userId);
            var projectNameFilter = Builders<Project>.Filter.Eq("Name", projectName);
            var result = await _collection.DeleteOneAsync(userIdFilter & projectNameFilter);
            return result.DeletedCount > 0;
        }
        public async Task<bool> DeleteAsync(int userId)
        {
            var projects = await _collection.Find(p => p.UserId == userId).ToListAsync();
            var ids = projects.Select(p => p.UserId);
            var userIdFilter = Builders<Project>.Filter.In(p => p.UserId, ids);
            var result = await _collection.DeleteManyAsync(userIdFilter);
            return result.DeletedCount > 0;
        }
        public async Task<IEnumerable<Project>> GetForUser(int userId)
        {
            var userIdFilter = Builders<Project>.Filter.Eq("UserId", userId);
            return await _collection.Find(userIdFilter).ToListAsync();
        }

        public async Task<Project> GetProject(int userId, string projectName)
        {
            var userIdFilter = Builders<Project>.Filter.Eq("UserId", userId);
            var projectNameFilter = Builders<Project>.Filter.Eq("Name", projectName);
            return await _collection.Find(userIdFilter & projectNameFilter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(int userId, string projectName, Project entity)
        {
            var userIdFilter = Builders<Project>.Filter.Eq("UserId", userId);
            var projectNameFilter = Builders<Project>.Filter.Eq("Name", projectName);
            var update = Builders<Project>.Update
                        .Set(p => p.UserId, entity.UserId)
                        .Set(p => p.Name, entity.Name)
                        .Set(p => p.Charts, entity.Charts);
            var result = await _collection.UpdateOneAsync(userIdFilter & projectNameFilter, update);
            return result.ModifiedCount > 0;
        }
        public async Task<List<IndicatorUsage>> GetTopUsedIndicators(List<int> userIds)
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("UserId", new BsonDocument("$in", new BsonArray(userIds)))),
                new BsonDocument("$unwind", "$Charts"),
                new BsonDocument("$unwind", "$Charts.Indicators"),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$Charts.Indicators.Name" },
                    { "Uses", new BsonDocument("$sum", 1) }
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    { "Name", "$_id" },
                    { "Uses", 1 },
                    { "_id", 0 }
                }),
                new BsonDocument("$sort", new BsonDocument("Uses", -1)),
                new BsonDocument("$limit", 3)
            };

            var result = await _collection.AggregateAsync<IndicatorUsage>(pipeline);
            return await result.ToListAsync();
        }
        public static async Task SeedDataAsync(IProjectsRepository projectsRepository)
        {
            // Check if the collection is empty
            if (!(await projectsRepository.GetAllAsync()).Any())
            {
                // Prepare seed data
                var seedData = new List<Project>
            {
                new Project
                {
                    UserId = 1,
                    Name = "Awesome Strategy",
                    Charts = new List<Chart>
                    {
                        new Chart
                        {
                            Symbol = Symbol.EURUSD,
                            Timeframe = Timeframe.M1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.MA, Parameters = "a=1;b=2;c=3" },
                                new Indicator { Name = IndicatorName.RSI, Parameters = "a=14" }
                            }
                        },
                        new Chart
                        {
                            Symbol = Symbol.USDJPY,
                            Timeframe = Timeframe.H1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.BB, Parameters = "a=20;b=2" }
                            }
                        }
                    }
                },
                new Project
                {
                    UserId = 2,
                    Name = "when lambo",
                    Charts = new List<Chart>
                    {
                        new Chart
                        {
                            Symbol = Symbol.EURUSD,
                            Timeframe = Timeframe.M1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.MA, Parameters = "a=1;b=2;c=3" },
                                new Indicator { Name = IndicatorName.RSI, Parameters = "a=14" }
                            }
                        },
                        new Chart
                        {
                            Symbol = Symbol.USDJPY,
                            Timeframe = Timeframe.H1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.MA, Parameters = "a=20;b=2" }
                            }
                        }
                    }
                },
                new Project
                {
                    UserId = 1,
                    Name = "new test",
                    Charts = new List<Chart>
                    {
                        new Chart
                        {
                            Symbol = Symbol.EURUSD,
                            Timeframe = Timeframe.M5,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.MA, Parameters = "a=1;b=2;c=3" },
                                new Indicator { Name = IndicatorName.RSI, Parameters = "a=14" }
                            }
                        },
                        new Chart
                        {
                            Symbol = Symbol.USDJPY,
                            Timeframe = Timeframe.H1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.MA, Parameters = "a=20;b=2" }
                            }
                        }
                    }
                },
                new Project
                {
                    UserId = 3,
                    Name = "new test",
                    Charts = new List<Chart>
                    {
                        new Chart
                        {
                            Symbol = Symbol.EURUSD,
                            Timeframe = Timeframe.M1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.MA, Parameters = "a=1;b=2;c=3" },
                                new Indicator { Name = IndicatorName.RSI, Parameters = "a=14" }
                            }
                        },
                        new Chart
                        {
                            Symbol = Symbol.USDJPY,
                            Timeframe = Timeframe.H1,
                            Indicators = new List<Indicator>
                            {
                                new Indicator { Name = IndicatorName.RSI, Parameters = "a=20;b=2" }
                            }
                        }
                    }
                }
            };

                // Insert seed data into the collection
                foreach (var userChart in seedData)
                {
                    await projectsRepository.CreateAsync(userChart);
                }
            }
        }

    }
}
