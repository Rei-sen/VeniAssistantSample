using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniAssistantSample.Function;

public class QueryFunction : FunctionTool
{
    public QueryFunction()
    {
        FunctionData = new()
        {
            Name = "query_venues",
            Description = "Search for venues by specified criteria",
            Parameters = new()
            {
                Required = new(),
                Properties = new()
                    {
                        {"Name", new ()
                        {
                                Type = "string",
                                Description = "The name of the venue to look for"
                            }
                        },
                        {"Manager", new()
                        {
                                Type = "string",
                                Description = "The manager of the venue to search for"
                            }
                        },
                        {"DataCenter", new()
                        {
                                Type = "string",
                                Description = "The data center on which the venue is located. Users can select multiple data centers at once (separating each with ','). Supported data centers are: Aether, Chaos, Crystal, Dynamis, Elemental, Gaia, Korea, Light, Mana, Materia, Meteor, Primal, 猫小胖, 莫古力, 豆豆柴, 陆行鸟."
                            }
                        },
                        {"World", new()
                        {
                                Type = "string",
                                Description = "The world on which the venue is located. Users can select multiple worlds at once (separating each with ','). Supported worlds are: Adamantoise, Aegis, Alexander, Anima, Asura, Atomos, Bahamut, Balmung, Behemoth, Belias, Brynhildr, Cactuar, Carbuncle, Cerberus, Chocobo, Coeurl, Diabolos, Durandal, Excalibur, Exodus, Faerie, Famfrit, Fenrir, Garuda, Gilgamesh, Goblin, Gungnir, Hades, Hyperion, Ifrit, Ixion, Jenova, Kujata, Lamia, Leviathan, Lich, Louisoix, Malboro, Mandragora, Masamune, Mateus, Midgardsormr, Moogle, Odin, Omega, Pandaemonium, Phoenix, Ragnarok, Ramuh, Ridill, Sargatanas, Shinryu, Shiva, Siren, Tiamat, Titan, Tonberry, Typhon, Ultima, Ultros, Unicorn, Valefor, Yojimbo, Zalera, Zeromus, Zodiark, Spriggan, Twintania, Bismarck, Ravana, Sephirot, Sophia, Zurvan, Halicarnassus, Maduin, Marilith, Seraph, HongYuHai, ShenYiZhiDi, LaNuoXiYa, HuanYingQunDao, MengYaChi, YuZhouHeYin, WoXianXiRan, ChenXiWangZuo, BaiYinXiang, BaiJinHuanXiang, ShenQuanHen, ChaoFengTing, LvRenZhanQiao, FuXiaoZhiJian, Longchaoshendian, MengYuBaoJing, ZiShuiZhanQiao, YanXia, JingYuZhuangYuan, MoDuNa, HaiMaoChaWu, RouFengHaiWan, HuPoYuan, ShuiJingTa2, YinLeiHu2, TaiYangHaiAn2, YiXiuJiaDe2, HongChaChuan2, Alpha, Phantom, Raiden, Sagittarius."
                            }
                        },
                        {"Tags", new()
                        {
                                Type = "string",
                                Description = "The tags of the venue to search for. Users can select multiple tags at once (separating each with ','). Common tags include: 'Courtesans', 'Gambling', 'Artists', 'Dancers', 'Bards', 'Food', 'Drink', 'Twitch DJ', 'Bar', 'Tarot', 'Pillow', 'Photography', 'Open stage', 'Void', 'Stylists', 'Performances', 'VIP', 'Triple triad', 'LGBTQIA+', 'RP Heavy', 'IC RP Only'"
                            }
                        },
                        {"HasBanner", new()
                        {
                                Type = "boolean",
                                Description = "Whether the venue has a banner or not"
                            }
                        },
                        {"Approved", new()
                        {
                                Type = "boolean",
                                Description = "Whether the venue is approved or not"
                            }
                        },
                        {"Open", new()
                        {
                                Type = "boolean",
                                Description = "Whether the venue is open or not"
                            }
                        },
                        {"WithinWeek", new()
                        {
                                Type = "boolean",
                                Description = "Whether the venue is open within the week or not"
                            }
                        },
                    }
            }
        };
    }
}
