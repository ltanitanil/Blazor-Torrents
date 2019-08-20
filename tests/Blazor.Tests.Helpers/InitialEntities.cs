using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Tests.Helpers
{
    public static class InitialEntities
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
                new Forum {Id = 4, Value = "Forum4"},
                new Forum {Id = 5, Value = "Forum5"},
                new Forum {Id = 6, Value = "Forum6"},
                new Forum {Id = 7, Value = "Forum7"},
                new Forum {Id = 8, Value = "Forum8"},
                new Forum {Id = 9, Value = "Forum9"},
                new Forum {Id = 10, Value = "Forum10"}
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
                new Torrent
                {
                    Id = 6,
                    Title = "Torrent6",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 666666,
                    ForumId = 2,
                    DirName = "Torrent6Dir",
                    Content = "Torrent6Content"
                },
                new Torrent
                {
                    Id = 7,
                    Title = "Torrent7",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 7777777,
                    ForumId = 3,
                    DirName = "Torrent7Dir",
                    Content = "Torrent7Content"
                },
                new Torrent
                {
                    Id = 8,
                    Title = "Torrent8",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 88888888,
                    ForumId = 5,
                    DirName = "Torrent8Dir",
                    Content = "Torrent8Content"
                },
                new Torrent
                {
                    Id = 9,
                    Title = "Torrent9",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 999999999,
                    ForumId = 2,
                    DirName = "Torrent9Dir",
                    Content = "Torrent9Content"
                },
                new Torrent
                {
                    Id = 10,
                    Title = "Torrent10",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 1,
                    ForumId = 2,
                    DirName = "Torrent10Dir",
                    Content = "Torrent10Content"
                },
                new Torrent
                {
                    Id = 11,
                    Title = "Torrent11",
                    RegisteredAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TrackerId = 1,
                    Hash = "67452301h",
                    Size = 11,
                    ForumId = 2,
                    DirName = "Torrent11Dir",
                    Content = "Torrent11Content"
                },
            };

        private static IEnumerable<File> GetFiles() =>
            new List<File>
            {
                new File {Id = 1, Name = "File1", Size = 10000000, TorrentId = 1},
                new File {Id = 2, Name = "File2", Size = 1000000, TorrentId = 1},
                new File {Id = 3, Name = "File3", Size = 1000000, TorrentId = 1},
                new File {Id = 4, Name = "File4", Size = 1000000, TorrentId = 2},
                new File {Id = 5, Name = "File5", Size = 1000000, TorrentId = 2},
                new File {Id = 6, Name = "File6", Size = 1000000, TorrentId = 2},
                new File {Id = 7, Name = "File7", Size = 1000000, TorrentId = 3},
                new File {Id = 8, Name = "File8", Size = 1000000, TorrentId = 3},
                new File {Id = 9, Name = "File9", Size = 1000000, TorrentId = 4},
                new File {Id = 10, Name = "File10", Size = 1000000, TorrentId = 5}
            };
    }
}
