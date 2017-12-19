﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.MatchEndpoint;
using OsuSharp.Misc;
using OsuSharp.ReplayEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserBestEndpoint;
using OsuSharp.UserEndpoint;

namespace OsuSharp
{
    public static class OsuApi
    {
        private const string RootDomain = "https://osu.ppy.sh";
        private const string GetBeatmapsUrl = "/api/get_beatmaps";
        private const string GetUserUrl = "/api/get_user";
        private const string GetScoresUrl = "/api/get_scores";
        private const string GetUserBestUrl = "/api/get_user_best";
        private const string GetUserRecentUrl = "/api/get_user_recent";
        private const string GetMatchUrl = "/api/get_match";
        private const string GetReplayUrl = "/api/get_replay";
        private const string ApiKeyParameter = "?k=";
        private const string UserParameter = "&u=";
        private const string MatchParameter = "&mp=";
        private const string LimitParameter = "&limit=";
        private const string BeatmapParameter = "&b=";

        private static HttpClient _httpClient;

        public static string ApiKey { get; set; }

        public static void Init(string apiKey)
        {
            ApiKey = apiKey;
            _httpClient = new HttpClient {BaseAddress = new Uri(RootDomain)};
        }

        public static async Task<Beatmaps> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string type = Beatmap.ToString(bmType);
            string request = await GetAsync($"{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{type}{beatmapId}{mode}");
            List<Beatmaps> r = JsonConvert.DeserializeObject<List<Beatmaps>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(string nickname, BeatmapType bmType = BeatmapType.ByCreator, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = Beatmap.ToString(bmType);
            string request = await GetAsync($"{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{type}{nickname}{LimitParameter}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = Beatmap.ToString(bmType);
            string request = await GetAsync($"{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{type}{id}{LimitParameter}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetLastBeatmapsAsync(int limit = 500)
        {
            string request = await GetAsync($"{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<Users> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetUserUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{username}{mode}");
            List<Users> r = JsonConvert.DeserializeObject<List<Users>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Users> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetUserUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{userid}{mode}");
            List<Users> r = JsonConvert.DeserializeObject<List<Users>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Scores> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetScoresUrl}{ApiKeyParameter}{ApiKey}{mode}{UserParameter}{username}{BeatmapParameter}{beatmapid}");
            List<Scores> r = JsonConvert.DeserializeObject<List<Scores>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Scores> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetScoresUrl}{ApiKeyParameter}{ApiKey}{mode}{UserParameter}{userid}{BeatmapParameter}{beatmapid}");
            List<Scores> r = JsonConvert.DeserializeObject<List<Scores>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<List<Scores>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetScoresUrl}{ApiKeyParameter}{ApiKey}{mode}{LimitParameter}{limit}{BeatmapParameter}{beatmapid}");
            return JsonConvert.DeserializeObject<List<Scores>>(request);
        }

        public static async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetUserBestUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{username}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetUserBestUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{userid}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserRecent.UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetUserRecentUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{username}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent.UserRecent>>(request);
        }

        public static async Task<List<UserRecent.UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetUserRecentUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{userid}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent.UserRecent>>(request);
        }

        public static async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            string request = await GetAsync($"{GetMatchUrl}{ApiKeyParameter}{ApiKey}{MatchParameter}{matchid}");
            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetReplayUrl}{ApiKeyParameter}{ApiKey}{mode}{BeatmapParameter}{beatmapid}{UserParameter}{username}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GetReplayUrl}{ApiKeyParameter}{ApiKey}{mode}{BeatmapParameter}{beatmapid}{UserParameter}{userid}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        private static async Task<string> GetAsync(string url)
        {
            HttpResponseMessage message = await _httpClient.GetAsync(url);
            if (message.StatusCode == HttpStatusCode.OK)
            {
                return await message.Content.ReadAsStringAsync();
            }
            throw new OsuSharpException(await message.Content.ReadAsStringAsync());
        }
    }
}
