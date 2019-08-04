using Blazor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Shared
{
    public class InitialEntities
    {
        public static readonly IEnumerable<Forum> Forums;
        public static readonly IEnumerable<Torrent> Torrents;
        public static readonly IEnumerable<File> Files;

        static InitialEntities()
        {
            Forums = GetForums();
            Torrents = GetTorrents();
            Files = GetFiles();
        }

        private static IEnumerable<Forum> GetForums() =>
            new List<Forum>
            {
                new Forum {Id = 1, Value = "Forum1"},
                new Forum {Id = 2, Value = "Forum2"},
                new Forum {Id = 3, Value = "Forum3"},
                new Forum {Id = 4, Value = "Forum3"}
            };

        private static IEnumerable<Torrent> GetTorrents() =>
            new List<Torrent>
            {
                new Torrent
                {
                    Id = 1,
                    Title = "Torrent1",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 1,
                    ForumId = 1,
                    DirName = "Torrent1Dir",
                    Content = "Torrent1Content"
                },
                new Torrent
                {
                    Id = 2,
                    Title = "Torrent2",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "78452301h",
                    Size = 22,
                    ForumId = 2,
                    DirName = "Torrent2Dir",
                    Content = "Torrent2Content"
                },
                new Torrent
                {
                    Id = 3,
                    Title = "Torrent3",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 333,
                    ForumId = 3,
                    DirName = "Torrent3Dir",
                    Content = "Torrent3Content"
                },
                new Torrent
                {
                    Id = 4,
                    Title = "Torrent4",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "78452301h",
                    Size = 4444,
                    ForumId = 4,
                    DirName = "Torrent4Dir",
                    Content = "Torrent4Content"
                },
                new Torrent
                {
                    Id = 5,
                    Title = "Torrent5",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 55555,
                    ForumId = 1,
                    DirName = "Torrent5Dir",
                    Content = "Torrent5Content"
                },
            };

        private static IEnumerable<File> GetFiles() =>
            new List<File>
            {
                new File {Id = 1, Name = "File1", Size = 10000000, TorrentId = 1},
                new File {Id = 2, Name = "File2", Size = 1000000, TorrentId = 1},
                new File {Id = 3, Name = "File2", Size = 1000000, TorrentId = 1},
                new File {Id = 4, Name = "File2", Size = 1000000, TorrentId = 2},
                new File {Id = 5, Name = "File2", Size = 1000000, TorrentId = 2},
                new File {Id = 6, Name = "File2", Size = 1000000, TorrentId = 2},
                new File {Id = 7, Name = "File2", Size = 1000000, TorrentId = 3},
                new File {Id = 8, Name = "File2", Size = 1000000, TorrentId = 3},
                new File {Id = 9, Name = "File2", Size = 1000000, TorrentId = 4},
                new File {Id = 10, Name = "File2", Size = 1000000, TorrentId = 5}
            };
    }
}
