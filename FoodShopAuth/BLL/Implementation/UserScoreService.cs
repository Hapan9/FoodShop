using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Implementation
{
    public class UserScoreService : IUserScoreService
    {
        private readonly HttpClient _client;

        private readonly Dictionary<int, float> _scoreRate = new()
        {
            {0, 0.1f},
            {1, 0.4f},
            {2, 0.6f},
            {3, 1f},
            {4, 0.8f}
        };

        private readonly IUserRepository _userRepository;

        public UserScoreService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _client = new HttpClient();
        }

        public async Task<float> GetUserScore(Guid id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
                throw new ArgumentNullException();


            if (user.ScoreCoefficient != null) return (float) user.ScoreCoefficient;
            user.ScoreCoefficient = 100f;
            await _userRepository.Update(user);
            await _userRepository.Save();

            return (float) user.ScoreCoefficient;
        }

        public async Task<IEnumerable<float>> GetUsersScore(IEnumerable<Guid> ids)
        {
            var scores = new List<float>();
            var users = (await _userRepository.GetAll()).Where(u => ids.Contains(u.Id)).ToList();
            var saveAction = false;

            foreach (var id in ids)
            {
                var usersSearch = users.Where(u => u.Id == id).ToList();

                if (!usersSearch.Any())
                    throw new ArgumentNullException();

                var user = usersSearch.First();
                if (user.ScoreCoefficient == null)
                {
                    user.ScoreCoefficient = 100f;
                    await _userRepository.Update(user);
                    saveAction = true;
                }

                scores.Add((float) user.ScoreCoefficient);
            }

            if (saveAction)
                await _userRepository.Save();

            return scores;
        }

        public async Task<float> UpdateUserScore(Guid id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
                throw new ArgumentNullException();

            var response = await _client.GetAsync($"https://localhost:44383/api/ProductsScore/{id}/User");
            response.EnsureSuccessStatusCode();

            var scores = await response.Content.ReadFromJsonAsync<IEnumerable<int>>();

            if (scores == null)
            {
                if (user.ScoreCoefficient != null)
                    return (float) user.ScoreCoefficient;

                user.ScoreCoefficient = 100;
            }
            else
            {
                var score = 0f;
                var scoresList = scores.ToList();
                score += _scoreRate[0] * scoresList.Count(s => s == 0);
                score += _scoreRate[1] * scoresList.Count(s => s == 1);
                score += _scoreRate[2] * scoresList.Count(s => s == 2);
                score += _scoreRate[3] * scoresList.Count(s => s == 3);
                score += _scoreRate[4] * scoresList.Count(s => s == 4);

                user.ScoreCoefficient = score / scoresList.Count * 100;
            }

            await _userRepository.Update(user);
            await _userRepository.Save();

            return (float) user.ScoreCoefficient;
        }
    }
}