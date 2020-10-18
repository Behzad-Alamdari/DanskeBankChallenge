using System;
using System.Collections.Generic;

namespace DBC.Models
{
    public static class SeedDataGenerator
    {
        public static List<Municipality> Generate()
        {
            var result = new List<Municipality>();
            result.Add(
                new Municipality
                {
                    Name = "Copenhagen",
                    TaxRules = new List<TaxRule>
                    {
                        new TaxRule
                        { Name = "Yearly", Priority = 1, Percentage = 0.2F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 1, 1), To = new DateTime(2020, 12, 31)}
                            }
                        },
                        new TaxRule
                        { Name = "Monthly", Priority = 2, Percentage = 0.4F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 5, 1), To = new DateTime(2020, 5, 31)}
                            }
                        },
                        new TaxRule
                        { Name = "Daily", Priority = 4, Percentage = 0.2F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 1, 1), To = new DateTime(2020, 1, 1)},
                                new Period{ From = new DateTime(2020, 12, 25), To = new DateTime(2020, 12, 25)}
                            }
                        }
                    }
                });

            result.Add(
                new Municipality
                {
                    Name = "Aarhus",
                    TaxRules = new List<TaxRule>
                    {
                        new TaxRule
                        { Name = "Yearly", Priority = 1, Percentage = 0.3F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 1, 1), To = new DateTime(2020, 12, 31)}
                            }
                        },
                        new TaxRule
                        { Name = "Monthly", Priority = 2, Percentage = 0.5F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 6, 1), To = new DateTime(2020, 6, 30)}
                            }
                        },
                        new TaxRule
                        { Name = "Weekly", Priority = 3, Percentage = 0.1F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 10, 12), To = new DateTime(2020, 10, 18)}
                            }
                        },
                        new TaxRule
                        { Name = "Daily", Priority = 4, Percentage = 0.2F, Periods = new List<Period>
                            {
                                new Period{ From = new DateTime(2020, 1, 1), To = new DateTime(2020, 1, 1)},
                                new Period{ From = new DateTime(2020, 12, 25), To = new DateTime(2020, 12, 25)}
                            }
                        }
                    }
                });

            return result;
        }
    }
}
