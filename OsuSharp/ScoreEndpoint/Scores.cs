﻿using Newtonsoft.Json;
using System;

namespace OsuSharp.ScoreEndpoint
{
    public class Scores
    {
        [JsonProperty("score")]
        public ulong Score { get; set; }

        [JsonProperty("score_id")]
        public ulong ScoreId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("count300")]
        public uint Count300 { get; set; }

        [JsonProperty("count100")]
        public uint Count100 { get; set; }

        [JsonProperty("count50")]
        public uint Count50 { get; set; }

        [JsonProperty("countmiss")]
        public uint Miss { get; set; }

        [JsonProperty("maxcombo")]
        public uint? MaxCombo { get; set; }

        [JsonProperty("countkatu")]
        public uint Katu { get; set; }

        [JsonProperty("countgeki")]
        public uint Geki { get; set; }

        [JsonProperty("perfect")]
        private ushort _Perfect;

        public bool Perfect
        {
            get { return Convert.ToBoolean(_Perfect); }
            set { Perfect = value; }
        }

        [JsonProperty("enabled_mods")]
        public uint Mods { get; set; }

        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("pp")]
        public float Pp { get; set; }
    }
}