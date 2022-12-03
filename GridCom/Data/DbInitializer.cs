using System;
using GridCom.Data;
using GridCom.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GridCom.Data

{
    public static class DbInitializer
    {
        public static void Initialize(GridContext context)
        {
            context.Database.EnsureCreated();
            if (context.RES.Any())
            {
                return;   // DB has been seeded
            }
            var res = new RES[]
        {
                new RES{Name_RES="АРЭС",FullName_RES="Апастовский РЭС",Adress_RES="Улица Мусы Джалиля, 23, Апастово, Республика Татарстан, 422350"},
                new RES{Name_RES="БРЭС",FullName_RES="Буинский РЭС",Adress_RES="Улица Космовского, 190, Буинск, Республика Татарстан, 422430"}

        };
            foreach (RES s in res)
            {
                context.RES.Add(s);
            }
            context.SaveChanges();
            var substations = new Substation[]
            {
                new Substation{Name_PS="Апастово",RESID=res.Single(s=>s.Name_RES=="ARES").RESID},
                new Substation{Name_PS="Каратун",RESID=1},
                 new Substation{Name_PS="Бакырчи",RESID=1},
                 new Substation{Name_PS="Ключи",RESID=1},
                  new Substation{Name_PS="Буинск",RESID=2},
                  new Substation{Name_PS="Западная",RESID=2},
                  new Substation{Name_PS="Раково",RESID=2},
                  new Substation{Name_PS="Киять",RESID=2},
                  new Substation{Name_PS="Энтуганы",RESID=2},
                  new Substation{Name_PS="Чечкабы",RESID=2},
                  new Substation{Name_PS="Рассвет",RESID=2},
                  new Substation{Name_PS="М.Цильна",RESID=2},
            };
            context.Substations.AddRange(substations);
            context.SaveChanges();

            var feeders = new Feeder[]
                {
                    new Feeder{Num_feeder=4, Extent_feeder=28.184,SubstationID=2 },
                    new Feeder{Num_feeder=5, Extent_feeder=17.841,SubstationID=2 },
                    new Feeder{Num_feeder=6, Extent_feeder=9.199,SubstationID=2 },

                    new Feeder{Num_feeder=3, Extent_feeder=2.4,SubstationID=1},
                    new Feeder{Num_feeder=6, Extent_feeder=22.936,SubstationID=1},
                    new Feeder{Num_feeder=7, Extent_feeder=37.115,SubstationID=1},

                    new Feeder{Num_feeder=1, Extent_feeder=20.99,SubstationID=3 },
                    new Feeder{Num_feeder=2, Extent_feeder=14.17,SubstationID=3},
                    new Feeder{Num_feeder=5, Extent_feeder=7.45,SubstationID=3},

                    new Feeder{Num_feeder=5, Extent_feeder=25.11,SubstationID=4},
                    new Feeder{Num_feeder=6, Extent_feeder=24.15,SubstationID=4},
                    new Feeder{Num_feeder=7, Extent_feeder=10.96,SubstationID=4 },
                     new Feeder{Num_feeder=8, Extent_feeder=8.4,SubstationID=4 },

                     new Feeder{Num_feeder=0, Extent_feeder=5.544,SubstationID=5 },
                    new Feeder{Num_feeder=2, Extent_feeder=7.826,SubstationID=5},

                    new Feeder{Num_feeder=3, Extent_feeder=25.762,SubstationID=6 },
                    new Feeder{Num_feeder=7, Extent_feeder=3.204,SubstationID=6},

                    new Feeder{Num_feeder=1, Extent_feeder=32.96,SubstationID=7 },
                    new Feeder{Num_feeder=6, Extent_feeder=15.924,SubstationID=7},

                    new Feeder{Num_feeder=1, Extent_feeder=23.468,SubstationID=8},
                    new Feeder{Num_feeder=5, Extent_feeder=25.24,SubstationID=8},

                     new Feeder{Num_feeder=1, Extent_feeder=0.84,SubstationID=9},
                    new Feeder{Num_feeder=2, Extent_feeder=8.47,SubstationID=9},

                     new Feeder{Num_feeder=6, Extent_feeder=54.057,SubstationID=10},
                    new Feeder{Num_feeder=7, Extent_feeder=35.72,SubstationID=10},

                     new Feeder{Num_feeder=2, Extent_feeder=23.617,SubstationID=11},
                    new Feeder{Num_feeder=6, Extent_feeder=17.55,SubstationID=11},

                     new Feeder{Num_feeder=3, Extent_feeder=24.731,SubstationID=12},
                    new Feeder{Num_feeder=11, Extent_feeder=2.94,SubstationID=12},
                };
            context.Feeders.AddRange(feeders);
            context.SaveChanges();

            var cases = new CasesOfOutage[]
                {
                    new CasesOfOutage{Case_outage="Дефект контактов разъединителя"},
                    new CasesOfOutage{Case_outage="Повреждение концевой кабельной муфты"},
                    new CasesOfOutage{Case_outage="Дефект шлейфа"},
                    new CasesOfOutage{Case_outage="Загрязненность изолятора"},
                    new CasesOfOutage{Case_outage="Наброс на провода ВЛ"},
                    new CasesOfOutage{Case_outage="Наличие следов перекрытия, оплавления"},
                    new CasesOfOutage{Case_outage="Неисправность в креплениях и соединениях проводов и тросов"},
                    new CasesOfOutage{Case_outage="Обрыв вязки"},

                     new CasesOfOutage{Case_outage="Вылет изолятора"},
                    new CasesOfOutage{Case_outage="Выпадение крюка (штыря)"},
                    new CasesOfOutage{Case_outage="Нарушение охранной зоны"},
                    new CasesOfOutage{Case_outage="Наличие ДКР в охранной зоне"},
                    new CasesOfOutage{Case_outage="Излом траверсы, штыря"},
                    new CasesOfOutage{Case_outage="Перекрытия вследствии воздействия птиц"},

                    new CasesOfOutage{Case_outage="Разрушение колпачка"},
                    new CasesOfOutage{Case_outage="Разрушение изолятора"},
                    new CasesOfOutage{Case_outage="Пробой изолятора"},

                    new CasesOfOutage{Case_outage="Разрушение стойки опор"},
                    new CasesOfOutage{Case_outage="Разрушение приставки"},
                    new CasesOfOutage{Case_outage="Обрыв провода"},

                   

                    new CasesOfOutage{Case_outage="В следствии дефекта на ТП"},
                    new CasesOfOutage{Case_outage="В следствии дефекта на потребительском оборудовании"},
                    new CasesOfOutage{Case_outage="Воздействие сторонних лиц"},
                    new CasesOfOutage{Case_outage="Разрушение разрядника или ОПН"},
                    new CasesOfOutage{Case_outage="Воздействие повторяющихся стихийных явлений"},
                    new CasesOfOutage{Case_outage="Невыявленная причина"},
                   
                };
            context.CasesOfOutage.AddRange(cases);
            context.SaveChanges();
            
            var outages = new Outage[]
            {
                
                new Outage{RESID=2,SubstationID=10,FeederID=24,Date_outage=DateTime.Parse("2020-03-01"),Downtime=TimeSpan.Parse("00:21:00"),UnderSupply=91,CasesOfOutageID=24,APV=false,RPV=true,OZZ=false},
                new Outage{RESID=2,SubstationID=12,FeederID=28,Date_outage=DateTime.Parse("2020-03-01"),Downtime=TimeSpan.Parse("00:53:00"),UnderSupply=149.6,CasesOfOutageID=24,APV=false,RPV=false,OZZ=false}
            };
            context.Outages.AddRange(outages);
            context.SaveChanges();
        }
    }
}
