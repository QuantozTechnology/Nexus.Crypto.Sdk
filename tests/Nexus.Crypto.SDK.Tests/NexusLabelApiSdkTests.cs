using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Tests.Helpers;
using Xunit;

namespace Nexus.Crypto.SDK.Tests;

public class NexusLabelApiSdkTests
{
    readonly LogicHelper _logicHelper;

    public NexusLabelApiSdkTests()
    {
        _logicHelper = new LogicHelper();
    }

    [Fact]
    public void GetCurrencies_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""baseCurrency"": {
                                ""name"": ""Euro"",
                                ""code"": ""EUR""
                            },
                            ""currencies"": [
                                {
                                    ""name"": ""Dollar"",
                                    ""code"": ""USD"",
                                    ""isActive"": true,
                                    ""rate"": 1.15741,
                                    ""rateUpdated"": ""2021-11-08T15:00:02Z""
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/currencies"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetCurrencies().Result;

        Assert.Null(response.Errors);
        Assert.Equal("Successfully processed your request", response.Message);

        Assert.Equal("Euro", response.Values.BaseCurrency.Name);
        Assert.Equal("EUR", response.Values.BaseCurrency.Code);

        Assert.Equal("Dollar", response.Values.Currencies.First().Name);
        Assert.Equal("USD", response.Values.Currencies.First().Code);
        Assert.True(response.Values.Currencies.First().IsActive);
        Assert.Equal(1.15741M, response.Values.Currencies.First().Rate);
        Assert.Equal("2021-11-08T15:00:02Z", response.Values.Currencies.First().RateUpdated);
    }

    [Fact]
    public void GetReserves_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""currencyReserves"": [
                                {
                                    ""code"": ""EUR"",
                                    ""name"": ""Euro"",
                                    ""type"": ""Exchange"",
                                    ""exchangeCode"": ""KRAKEN"",
                                    ""total"": 0.1,
                                    ""locked"": 0.2,
                                    ""available"": 0.3,
                                    ""unconfirmed"": 0.4,
                                    ""updated"": ""2020-09-07T00:00:00""
                                }
                            ],
                            ""cryptoReserves"": [
                                {
                                    ""code"": ""XLM"",
                                    ""name"": ""Lumens"",
                                    ""type"": ""Exchange"",
                                    ""exchangeCode"": ""KRAKEN"",
                                    ""total"": 1.0,
                                    ""locked"": 0.0001,
                                    ""available"": 0.9999,
                                    ""unconfirmed"": 0.0,
                                    ""updated"": ""2020-08-19T00:00:00""
                                },
                                {
                                    ""code"": ""ETH"",
                                    ""name"": null,
                                    ""type"": ""Coldstore"",
                                    ""exchangeCode"": null,
                                    ""total"": 0.00000001,
                                    ""locked"": 0.0,
                                    ""available"": 0.00000001,
                                    ""unconfirmed"": null,
                                    ""updated"": ""2021-08-12T12:29:59""
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get,
                new Uri("https://api.quantoznexus.com/reserves?reservesTimeStamp=2020-08-24T01:02:04Z"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetReserves("2020-08-24T01:02:04Z").Result;
        Assert.Null(response.Errors);
        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Equal("EUR", response.Values.CurrencyReserves.First().Code);
        Assert.Equal("Euro", response.Values.CurrencyReserves.First().Name);
        Assert.Equal("Exchange", response.Values.CurrencyReserves.First().Type);
        Assert.Equal("KRAKEN", response.Values.CurrencyReserves.First().ExchangeCode);
        Assert.Equal(0.1M, response.Values.CurrencyReserves.First().Total);
        Assert.Equal(0.2M, response.Values.CurrencyReserves.First().Locked);
        Assert.Equal(0.3M, response.Values.CurrencyReserves.First().Available);
        Assert.Equal(0.4M, response.Values.CurrencyReserves.First().Unconfirmed);
        Assert.Equal("2020-09-07T00:00:00", response.Values.CurrencyReserves.First().Updated);
        Assert.Equal("EUR", response.Values.CurrencyReserves.First().Code);

        Assert.Equal("Lumens", response.Values.CryptoReserves.First(x => x.Code == "XLM").Name);
        Assert.Equal("Exchange", response.Values.CryptoReserves.First(x => x.Code == "XLM").Type);
        Assert.Equal("KRAKEN", response.Values.CryptoReserves.First(x => x.Code == "XLM").ExchangeCode);
        Assert.Equal(1.0M, response.Values.CryptoReserves.First(x => x.Code == "XLM").Total);
        Assert.Equal(0.0001M, response.Values.CryptoReserves.First(x => x.Code == "XLM").Locked);
        Assert.Equal(0.9999M, response.Values.CryptoReserves.First(x => x.Code == "XLM").Available);
        Assert.Equal(0.0M, response.Values.CryptoReserves.First(x => x.Code == "XLM").Unconfirmed);
        Assert.Equal("2020-08-19T00:00:00", response.Values.CryptoReserves.First(x => x.Code == "XLM").Updated);

        Assert.Null(response.Values.CryptoReserves.First(x => x.Code == "ETH").Name);
        Assert.Equal("Coldstore", response.Values.CryptoReserves.First(x => x.Code == "ETH").Type);
        Assert.Null(response.Values.CryptoReserves.First(x => x.Code == "ETH").ExchangeCode);
        Assert.Equal(0.00000001M, response.Values.CryptoReserves.First(x => x.Code == "ETH").Total);
        Assert.Equal(0.0M, response.Values.CryptoReserves.First(x => x.Code == "ETH").Locked);
        Assert.Equal(0.00000001M, response.Values.CryptoReserves.First(x => x.Code == "ETH").Available);
        Assert.Null(response.Values.CryptoReserves.First(x => x.Code == "ETH").Unconfirmed);
        Assert.Equal("2021-08-12T12:29:59", response.Values.CryptoReserves.First(x => x.Code == "ETH").Updated);
    }

    [Fact]
    public void GetCustodianBalances_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""currencyBalances"": [
                                {
                                    ""code"": ""EUR"",
                                    ""name"": ""Euro"",
                                    ""locked"": 0.1,
                                    ""total"": 0.3,
                                    ""available"": 0.2
                                }
                            ],
                            ""cryptoBalances"": [
                                {
                                    ""code"": ""XLM"",
                                    ""name"": ""Lumens"",
                                    ""locked"": 0.2,
                                    ""total"": -44.64180159,
                                    ""available"": -44.8
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/balances"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetCustodianBalances().Result;
        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        Assert.Equal("EUR", response.Values.CurrencyBalances.First().Code);
        Assert.Equal("Euro", response.Values.CurrencyBalances.First().Name);
        Assert.Equal(0.1M, response.Values.CurrencyBalances.First().Locked);
        Assert.Equal(0.3M, response.Values.CurrencyBalances.First().Total);
        Assert.Equal(0.2M, response.Values.CurrencyBalances.First().Available);

        Assert.Equal("XLM", response.Values.CryptoBalances.First().Code);
        Assert.Equal("Lumens", response.Values.CryptoBalances.First().Name);
        Assert.Equal(0.2M, response.Values.CryptoBalances.First().Locked);
        Assert.Equal(-44.64180159M, response.Values.CryptoBalances.First().Total);
        Assert.Equal(-44.8M, response.Values.CryptoBalances.First().Available);
    }

    [Fact]
    public void GetMails_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""page"": 1,
                            ""total"": 6,
                            ""totalPages"": 1,
                            ""filteringParameters"": {
                                ""startDate"": ""2020-06-21T16:24:09Z"",
                                ""endDate"": ""2022-06-21T16:24:09Z"",
                                ""accountCode"": ""CJABM2HM"",
                                ""customerCode"": ""NLTEST"",
                                ""status"": ""ReadyToSend"",
                                ""type"": ""TransactionToBeReturned"",
                                ""sortBy"": ""Created"",
                                ""sortDirection"": ""Desc""
                            },
                            ""records"": [
                                {
                                    ""created"": ""2021-11-04T08:12:59Z"",
                                    ""code"": ""206b3f79-5536-43df-874c-9a2410ea7f4d"",
                                    ""sent"": """",
                                    ""status"": ""ReadyToSend"",
                                    ""type"": ""TransactionToBeReturned"",
                                    ""references"": {
                                        ""accountCode"": ""CJABM2HM"",
                                        ""customerCode"": ""NLTEST"",
                                        ""transactionCode"": ""DT20211104081259S7V""
                                    },
                                    ""content"": {
                                        ""subject"": null,
                                        ""html"": null,
                                        ""text"": null
                                    },
                                    ""recipient"": {
                                        ""email"": ""raymen@quantoz.com                                                              "",
                                        ""cc"": null,
                                        ""bcc"": null
                                    }
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri(
                "https://api.quantoznexus.com/mail?startDate=2020-06-21T16:24:09Z" +
                "&endDate=2022-06-21T16:24:09Z&accountCode=CJABM2HM&customerCode=NLTEST&status=ReadyToSend&type=TransactionToBeReturned"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });


        var response = _logicHelper.ApiService.GetMails(new System.Collections.Generic.Dictionary<string, string>
        {
            { "startDate", "2020-06-21T16:24:09Z" },
            { "endDate", "2022-06-21T16:24:09Z" },
            { "accountCode", "CJABM2HM" },
            { "customerCode", "NLTEST" },
            { "status", "ReadyToSend" },
            { "type", "TransactionToBeReturned" }
        }).Result;

        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        // We don't test pass trough of all data from the endpoint, because the SDK does not support it yet.

        Assert.Equal(1, response.Values.Page);
        Assert.Equal(6, response.Values.Total);
        Assert.Equal(1, response.Values.TotalPages);

        var record1 = response.Values.Records.First();

        Assert.Equal("2021-11-04T08:12:59Z", record1.Created);
        Assert.Equal("206b3f79-5536-43df-874c-9a2410ea7f4d", record1.Code);
        Assert.Equal("", record1.Sent);
        Assert.Equal("ReadyToSend", record1.Status);
        Assert.Equal("TransactionToBeReturned", record1.Type);
        Assert.Equal("CJABM2HM", record1.References.AccountCode);
        Assert.Equal("NLTEST", record1.References.CustomerCode);
        Assert.Equal("DT20211104081259S7V", record1.References.TransactionCode);
        Assert.Null(record1.Content.Subject);
        Assert.Null(record1.Content.Html);
        Assert.Null(record1.Content.Text);
        Assert.Equal("raymen@quantoz.com                                                              ",
            record1.Recipient.Email);
        Assert.Null(record1.Recipient.CC);
        Assert.Null(record1.Recipient.BCC);
    }

    [Fact]
    public void GetBrokerBalances_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""XLM"": {
                            ""balanceCurrencyCode"": ""XLM"",
                            ""availableBalance"": 14729.80374332,
                            ""unconfirmedBalance"": 14729.75757575,
                            ""hotConfirmedBalance"": 9647.3783601,
                            ""delayedSending"": 0.0,
                            ""coldStoreBalance"": 0.3,
                            ""custodianBalance"": null,
                            ""nexusTickerPrice"": 0.3256714,
                            ""nexusPriceUpdated"": ""2021-08-12T13:07:40"",
                            ""currencies"": [
                                {
                                    ""code"": ""EUR"",
                                    ""updated"": ""2021-08-12T13:00:04"",
                                    ""ask"": 0.0,
                                    ""bid"": 0.0,
                                    ""rates"": [
                                        {
                                            ""code"": ""USD"",
                                            ""rate"": 1.17384,
                                            ""updated"": ""2021-08-12T13:00:04""
                                        }
                                    ]
                                }
                            ]
                        },
                        ""LTC"": {
                            ""balanceCurrencyCode"": ""LTC"",
                            ""availableBalance"": 0.1,
                            ""unconfirmedBalance"": 0.2,
                            ""hotConfirmedBalance"": 0.3,
                            ""delayedSending"": 0.4,
                            ""coldStoreBalance"": 0.5,
                            ""custodianBalance"": null,
                            ""nexusTickerPrice"": 166.244215,
                            ""nexusPriceUpdated"": ""2021-08-12T13:07:40"",
                            ""currencies"": [
                                {
                                    ""code"": ""USD"",
                                    ""updated"": ""2021-08-12T13:00:04"",
                                    ""ask"": 167.0,
                                    ""bid"": 165.0,
                                    ""rates"": [
                                        {
                                            ""code"": ""EUR"",
                                            ""rate"": 0.85255,
                                            ""updated"": ""2021-08-12T13:00:04""
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/labelpartner/balance"))
            {
                Headers = { { "api_version", "1.1" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetBrokerBalances().Result;

        // We don't test pass trough of all data from the endpoint, because the SDK does not support it yet.

        var xlmBalance = response.Values.Balances.Single(x => x.BalanceCurrencyCode == "XLM");

        Assert.Equal(14729.80374332, xlmBalance.AvailableBalance);
        Assert.Equal(14729.75757575, xlmBalance.UnconfirmedBalance);
        Assert.Equal(9647.3783601, xlmBalance.HotConfirmedBalance);
        Assert.Equal(0.0, xlmBalance.DelayedSending);
        Assert.Equal(0.3M, xlmBalance.ColdStoreBalance);

        var ltcBalance = response.Values.Balances.Single(x => x.BalanceCurrencyCode == "LTC");

        Assert.Equal(0.1, ltcBalance.AvailableBalance);
        Assert.Equal(0.2, ltcBalance.UnconfirmedBalance);
        Assert.Equal(0.3, ltcBalance.HotConfirmedBalance);
        Assert.Equal(0.4, ltcBalance.DelayedSending);
        Assert.Equal(0.5M, ltcBalance.ColdStoreBalance);
    }

    [Fact]
    public void GetBalancesMutations_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""page"": 1,
                            ""total"": 116,
                            ""totalPages"": 3,
                            ""filteringParameters"": {
                                ""cryptoCode"": ""XLM""
                            },
                            ""records"": [
                                {
                                    ""type"": ""GIFT"",
                                    ""created"": ""2021-09-27T14:21:22Z"",
                                    ""transactionCode"": ""DT20210920141806GSP"",
                                    ""cryptoAmount"": -1171.81091659,
                                    ""cryptoCode"": ""XLM"",
                                    ""value"": -334.90355996156455
                                },
                                {
                                    ""type"": ""FEE"",
                                    ""created"": ""2021-09-27T14:21:23Z"",
                                    ""transactionCode"": ""DT20210920141806GSQ"",
                                    ""cryptoAmount"": -1E-05,
                                    ""cryptoCode"": ""XLM"",
                                    ""value"": -2.8580000000000004E-06
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get,
                new Uri("https://api.quantoznexus.com/balances/hotwallet/mutations?cryptoCode=XLM"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService
            .GetBalanceMutations(
                new System.Collections.Generic.Dictionary<string, string> { { "cryptoCode", "XLM" } }).Result;


        var giftMutation = response.Values.Records.Single(x => x.Type == "GIFT");

        Assert.Equal("2021-09-27T14:21:22Z", giftMutation.Created);
        Assert.Equal("DT20210920141806GSP", giftMutation.TransactionCode);
        Assert.Equal(-1171.81091659M, giftMutation.CryptoAmount);
        Assert.Equal("XLM", giftMutation.CryptoCode);
        Assert.Equal(-334.90355996156455M, giftMutation.Value);

        var feeMutation = response.Values.Records.Single(x => x.Type == "FEE");

        Assert.Equal("2021-09-27T14:21:23Z", feeMutation.Created);
        Assert.Equal("DT20210920141806GSQ", feeMutation.TransactionCode);
        Assert.Equal(-1E-05M, feeMutation.CryptoAmount);
        Assert.Equal("XLM", feeMutation.CryptoCode);
        Assert.Equal(-2.8580000000000004E-06M, feeMutation.Value);
    }

    [Fact]
    public void GetTransfers_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""page"": 1,
                            ""total"": 59,
                            ""totalPages"": 59,
                            ""filteringParameters"": {
                                ""sinkType"": ""HotWallet"",
                                ""sourceExchangeCode"": ""KRAKEN""
                            },
                            ""records"": [
                                {
                                    ""transferCode"": ""TFR-DC20200921091445TFPP"",
                                    ""type"": ""DEPOSIT"",
                                    ""status"": ""COMPLETED"",
                                    ""amount"": 199.99998000,
                                    ""created"": ""2020-09-21T09:14:45"",
                                    ""finished"": ""2020-09-21T09:15:12"",
                                    ""exchangeTransferCode"": null,
                                    ""price"": 0.06367,
                                    ""txId"": ""136233420576702465"",
                                    ""userName"": null,
                                    ""comment"": ""other"",
                                    ""cryptoCode"": ""XLM"",
                                    ""address"": {
                                        ""sourceType"": ""Exchange"",
                                        ""sinkType"": ""HotWallet"",
                                        ""address"": ""GDUZG3G6T276CGLKZVRLPI7SVO3UEP3L67JEG6FO3E2HH4CHJCJRH3GB"",
                                        ""sinkExchangeCode"": null,
                                        ""sourceExchangeCode"": ""KRAKEN""
                                    }
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/transfers?" +
                                                           "sinkType=HotWallet&sourceExchangeCode=KRAKEN&limit=1"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService
            .GetTransfers(new System.Collections.Generic.Dictionary<string, string>
            {
                { "sinkType", "HotWallet" }, { "sourceExchangeCode", "KRAKEN" }, { "limit", "1" }
            }).Result;

        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        Assert.Equal(1, response.Values.Page);
        Assert.Equal(59, response.Values.Total);
        Assert.Equal(59, response.Values.TotalPages);
        Assert.Equal(2, response.Values.FilteringParameters.Count);
        Assert.Equal("TFR-DC20200921091445TFPP", response.Values.Records.Single().TransferCode);
        Assert.Equal("DEPOSIT", response.Values.Records.Single().Type);
        Assert.Equal("COMPLETED", response.Values.Records.Single().Status);
        Assert.Equal(199.99998000M, response.Values.Records.Single().Amount);
        Assert.Equal("2020-09-21T09:14:45", response.Values.Records.Single().Created);
        Assert.Equal("2020-09-21T09:15:12", response.Values.Records.Single().Finished);
        Assert.Null(response.Values.Records.Single().ExchangeTransferCode);
        Assert.Equal(0.06367m, response.Values.Records.Single().Price);
        Assert.Equal("136233420576702465", response.Values.Records.Single().TxId);
        Assert.Null(response.Values.Records.Single().UserName);
        Assert.Equal("other", response.Values.Records.Single().Comment);
        Assert.Equal("XLM", response.Values.Records.Single().CryptoCode);

        Assert.Equal("Exchange", response.Values.Records.Single().Address.SourceType);
        Assert.Equal("HotWallet", response.Values.Records.Single().Address.SinkType);
        Assert.Equal("GDUZG3G6T276CGLKZVRLPI7SVO3UEP3L67JEG6FO3E2HH4CHJCJRH3GB",
            response.Values.Records.Single().Address.Address);
        Assert.Null(response.Values.Records.Single().Address.SinkExchangeCode);
        Assert.Equal("KRAKEN", response.Values.Records.Single().Address.SourceExchangeCode);
    }

    [Fact]
    public void GetCustomer_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""customerCode"": ""NL29RABO3365135561"",
                            ""email"": ""cust1@mail.com"",
                            ""created"": ""2021-03-02T10:13:03"",
                            ""bankAccount"": ""NL19ING4918734561"",
                            ""bankAccountName"": ""ING"",
                            ""status"": ""ACTIVE"",
                            ""trustlevel"": ""New"",
                            ""portFolioCode"": ""ABC"",
                            ""isHighRisk"": true,
                            ""isBusiness"": true,
                            ""hasPhotoId"": true,
                            ""countryCode"": ""NL"",
                            ""comment"": ""no comment"",
                            ""data"": null
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get,
                new Uri("https://api.quantoznexus.com/customer/NL29RABO3365135561"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetCustomer("NL29RABO3365135561").Result;

        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        Assert.Equal("NL29RABO3365135561", response.Values.CustomerCode);
        Assert.Equal("cust1@mail.com", response.Values.Email);
        Assert.Equal("2021-03-02T10:13:03", response.Values.Created);
        Assert.Equal("NL19ING4918734561", response.Values.BankAccount);
        Assert.Equal("ING", response.Values.BankAccountName);
        Assert.Equal("ACTIVE", response.Values.Status);
        Assert.Equal("New", response.Values.Trustlevel);
        Assert.Equal("ABC", response.Values.PortFolioCode);
        Assert.True(response.Values.IsHighRisk);
        Assert.True(response.Values.IsBusiness);
        Assert.True(response.Values.HasPhotoId);
        Assert.Equal("NL", response.Values.CountryCode);
        Assert.Equal("no comment", response.Values.Comment);
        Assert.Null(response.Values.Data);
    }

    [Fact]
    public void GetCustomers_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""page"": 1,
                            ""total"": 1,
                            ""totalPages"": 1,
                            ""filteringParameters"": {
                                ""startDate"": ""2021-01-01T00:00:01Z"",
                                ""endDate"": ""2022-01-01T00:00:03Z"",
                                ""status"": ""Active""
                            },
                            ""records"": [
                                {
                                    ""customerCode"": ""NL29RABO3365135561"",
                                    ""email"": ""cust1@mail.com"",
                                    ""created"": ""2021-03-02T10:13:03"",
                                    ""bankAccount"": ""NL19ING4918734561"",
                                    ""bankAccountName"": ""ING"",
                                    ""status"": ""ACTIVE"",
                                    ""trustlevel"": ""New"",
                                    ""portFolioCode"": ""ABC"",
                                    ""isHighRisk"": true,
                                    ""isBusiness"": true,
                                    ""hasPhotoId"": true,
                                    ""countryCode"": ""NL"",
                                    ""comment"": ""no comment"",
                                    ""data"": null
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get,
                new Uri(
                    "https://api.quantoznexus.com/customer?startDate=2021-01-01T00:00:01Z&endDate=2022-01-01T00:00:03Z&status=Active"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService
            .GetCustomers(new System.Collections.Generic.Dictionary<string, string>
            {
                { "startDate", "2021-01-01T00:00:01Z" },
                { "endDate", "2022-01-01T00:00:03Z" },
                { "status", "Active" }
            }).Result;

        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        Assert.Equal("2021-01-01T00:00:01Z",
            response.Values.FilteringParameters.Single(x => x.Key == "startDate").Value);
        Assert.Equal("2022-01-01T00:00:03Z",
            response.Values.FilteringParameters.Single(x => x.Key == "endDate").Value);
        Assert.Equal("Active", response.Values.FilteringParameters.Single(x => x.Key == "status").Value);

        Assert.Equal("NL29RABO3365135561", response.Values.Records.First().CustomerCode);
        Assert.Equal("cust1@mail.com", response.Values.Records.First().Email);
        Assert.Equal("2021-03-02T10:13:03", response.Values.Records.First().Created);
        Assert.Equal("NL19ING4918734561", response.Values.Records.First().BankAccount);
        Assert.Equal("ING", response.Values.Records.First().BankAccountName);
        Assert.Equal("ACTIVE", response.Values.Records.First().Status);
        Assert.Equal("New", response.Values.Records.First().Trustlevel);
        Assert.Equal("ABC", response.Values.Records.First().PortFolioCode);
        Assert.True(response.Values.Records.First().IsHighRisk);
        Assert.True(response.Values.Records.First().IsBusiness);
        Assert.True(response.Values.Records.First().HasPhotoId);
        Assert.Equal("NL", response.Values.Records.First().CountryCode);
        Assert.Equal("no comment", response.Values.Records.First().Comment);
        Assert.Null(response.Values.Records.First().Data);
    }

    [Fact]
    public async void GetCustomers_InvalidQuery()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Could not parse abc"",
                        ""errors"": [
                            ""InvalidQueryParameter""
                        ],
                        ""values"": {
                            ""validCustomerStatus"": [
                                ""NEW"",
                                ""ACTIVE"",
                                ""DELETED"",
                                ""UNDERREVIEW"",
                                ""BLOCKED""
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/customer?status=abc"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var e = await Assert.ThrowsAsync<NexusApiException>(async () => await _logicHelper.ApiService.GetCustomers(
            new System.Collections.Generic.Dictionary<string, string> { { "status", "abc" } }));

        Assert.Equal(mockResponseBody, e.ResponseContent);
        Assert.Equal(HttpStatusCode.BadRequest, e.StatusCode);
    }

    [Fact]
    public void GetPrices_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                                    ""created"": ""2021-08-11T10:14:24"",
                            ""currencyCode"": ""EUR"",
                            ""prices"": {
                                        ""ETH"": {
                                            ""buy"": 2771.87222,
                                    ""sell"": 2744.2914,
                                    ""estimatedNetworkSlowFee"": 0.03,
                                    ""estimatedNetworkFastFee"": 0.03,
                                    ""updated"": ""2021-08-11T10:14:10.997""
                                        },
                                ""BCH"": {
                                            ""buy"": 523.95648,
                                    ""sell"": 518.74298,
                                    ""estimatedNetworkSlowFee"": null,
                                    ""estimatedNetworkFastFee"": null,
                                    ""updated"": ""2021-08-11T10:14:10.973""
                                }
                            }
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/prices/eur"))
            {
                Headers = { { "api_version", "1.2" } }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        using (var client = _logicHelper.ApiClientFactory.GetClient(null))
        {
            var response = _logicHelper.ApiService.GetPrices("eur").Result;
            Assert.Null(response.Errors);
            Assert.Equal("Successfully processed your request", response.Message);
            Assert.Equal("2021-08-11T10:14:24", response.Values.Created);
            Assert.Equal("EUR", response.Values.CurrencyCode);
            Assert.Equal(2, response.Values.Prices.Count);
            Assert.Equal(2771.87222, response.Values.Prices["ETH"].Buy);
            Assert.Equal(2744.2914, response.Values.Prices["ETH"].Sell);
            Assert.Equal(0.03, response.Values.Prices["ETH"].EstimatedNetworkSlowFee);
            Assert.Equal(0.03, response.Values.Prices["ETH"].EstimatedNetworkFastFee);
            Assert.Equal(DateTime.Parse("2021-08-11T10:14:10.997"), response.Values.Prices["ETH"].Updated);
            Assert.Equal(523.95648, response.Values.Prices["BCH"].Buy);
            Assert.Equal(518.74298, response.Values.Prices["BCH"].Sell);
            Assert.Null(response.Values.Prices["BCH"].EstimatedNetworkSlowFee);
            Assert.Null(response.Values.Prices["BCH"].EstimatedNetworkFastFee);
            Assert.Equal(DateTime.Parse("2021-08-11T10:14:10.973"), response.Values.Prices["BCH"].Updated);
        }
    }
}