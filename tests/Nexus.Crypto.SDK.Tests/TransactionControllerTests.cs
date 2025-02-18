using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Nexus.Crypto.SDK.Tests.Helpers;
using Nexus.LabelApi.SDK.Models;
using Nexus.LabelApi.SDK.Models.Response;
using Xunit;

namespace Nexus.Crypto.SDK.Tests;

public class TransactionControllerTests
{
    readonly LogicHelper _logicHelper;

    public TransactionControllerTests()
    {
        _logicHelper = new LogicHelper();
    }

    [Fact]
    public void GetTransactions_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""page"": 1,
                            ""total"": 18,
                            ""totalPages"": 1,
                            ""filteringParameters"": {
                                ""customer"": ""NL51INGB7243913512"",
                                ""customerStatus"": ""Active"",
                                ""status"": ""BLOCKED|PAYOUTONHOLD"",
                                ""type"": ""SELL"",
                                ""customerIsHighrisk"": ""False"",
                                ""customerIsBusiness"": ""False"",
                                ""customerTrustlevel"": ""Trusted"",
                                ""isSettled"": ""False"",
                                ""startDate"": ""2020-06-04T14:52:41Z"",
                                ""endDate"": ""2022-06-04T14:52:41Z""
                            },
                            ""records"": [
                                {
                                    ""created"": ""2021-06-04T14:52:41"",
                                    ""transactionCode"": ""DT20210604145241SL6"",
                                    ""transactionType"": ""SELL"",
                                    ""status"": ""PayoutOnHold"",
                                    ""type"": ""SELL"",
                                    ""portfolioCode"": null,
                                    ""customerCode"": ""NL51INGB7243913512"",
                                    ""accountCode"": ""XYFWTUQF"",
                                    ""cryptoCurrencyCode"": ""XLM"",
                                    ""currencyCode"": ""EUR"",
                                    ""enchangeCode"": ""XLMWALLETDUROTAN"",
                                    ""paymentMethodCode"": ""DT_CRYPTO_SELL_XLM_EUR"",
                                    ""exchangeOrderCode"": null,
                                    ""cryptoSendTxId"": null,
                                    ""cryptoReceiveTxId"": ""Hic dolor qui et quas unde enim."",
                                    ""notified"": ""2021-06-04T14:52:41"",
                                    ""traded"": ""2021-06-04T14:52:42"",
                                    ""confirmed"": ""2021-06-04T14:52:43"",
                                    ""finished"": null,
                                    ""comment"": ""Exceeded maximum allowed transaction value set by DailySellLimit"",
                                    ""cryptoAmount"": 30.0,
                                    ""cryptoSent"": null,
                                    ""cryptoTraded"": 30.0,
                                    ""cryptoExpectedAmount"": 0.312140214, 
                                    ""cryptoEstimatePrice"": 0.312140214,
                                    ""cryptoTradePrice"": 0.312140214,
                                    ""cryptoPrice"": 0.312140214,
                                    ""tradeValue"": 9.36396,
                                    ""bankCommission"": 0.8214,
                                    ""partnerCommission"": 0.1404594,
                                    ""networkCommission"": 0.1,
                                    ""payout"": 8.402100599999999,
                                    ""payComment"": null,
                                    ""bankTransferReference"": null,
                                    ""isSettled"": true
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/transaction?status=BLOCKED|PAYOUTONHOLD" +
                "&customer=NL51INGB7243913512&customerIsHighrisk=false&customerIsBusiness=False&customerTrustlevel=Trusted&customerStatus=Active" +
                "&startDate=2020-06-04T14:52:41Z&endDate=2022-06-04T14:52:41Z&type=SELL&isSettled=false"))
            {
                Headers = {
                    { "api_version", "1.2" }
                }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetTransactions(new System.Collections.Generic.Dictionary<string, string>
        {
            {"status", "BLOCKED|PAYOUTONHOLD"},
            {"customer", "NL51INGB7243913512"},
            {"customerIsHighrisk", "false"},
            {"customerIsBusiness", "False"},
            {"customerTrustlevel", "Trusted"},
            {"customerStatus", "Active"},
            {"startDate", "2020-06-04T14:52:41Z"},
            {"endDate", "2022-06-04T14:52:41Z"},
            {"type", "SELL"},
            {"isSettled", "false"}
        }).Result;

        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        Assert.Equal("NL51INGB7243913512", response.Values.FilteringParameters.Single(x => x.Key == "customer").Value);
        Assert.Equal("Active", response.Values.FilteringParameters.Single(x => x.Key == "customerStatus").Value);
        Assert.Equal("BLOCKED|PAYOUTONHOLD", response.Values.FilteringParameters.Single(x => x.Key == "status").Value);
        Assert.Equal("SELL", response.Values.FilteringParameters.Single(x => x.Key == "type").Value);
        Assert.Equal("False", response.Values.FilteringParameters.Single(x => x.Key == "customerIsHighrisk").Value);
        Assert.Equal("False", response.Values.FilteringParameters.Single(x => x.Key == "customerIsBusiness").Value);
        Assert.Equal("Trusted", response.Values.FilteringParameters.Single(x => x.Key == "customerTrustlevel").Value);
        Assert.Equal("False", response.Values.FilteringParameters.Single(x => x.Key == "isSettled").Value);
        Assert.Equal("2020-06-04T14:52:41Z", response.Values.FilteringParameters.Single(x => x.Key == "startDate").Value);
        Assert.Equal("2022-06-04T14:52:41Z", response.Values.FilteringParameters.Single(x => x.Key == "endDate").Value);

        Assert.Equal("2021-06-04T14:52:41", response.Values.Records.First().Created);
        Assert.Equal("DT20210604145241SL6", response.Values.Records.First().TransactionCode);
        Assert.Equal("SELL", response.Values.Records.First().TransactionType);
        Assert.Equal("PayoutOnHold", response.Values.Records.First().Status);
        Assert.Equal("SELL", response.Values.Records.First().Type);
        Assert.Null(response.Values.Records.First().PortfolioCode);
        Assert.Equal("NL51INGB7243913512", response.Values.Records.First().CustomerCode);
        Assert.Equal("XYFWTUQF", response.Values.Records.First().AccountCode);
        Assert.Equal("XLM", response.Values.Records.First().CryptoCurrencyCode);
        Assert.Equal("EUR", response.Values.Records.First().CurrencyCode);
        Assert.Equal("XLMWALLETDUROTAN", response.Values.Records.First().ExchangeCode);
        Assert.Equal("DT_CRYPTO_SELL_XLM_EUR", response.Values.Records.First().PaymentMethodCode);
        Assert.Null(response.Values.Records.First().ExchangeOrderCode);
        Assert.Null(response.Values.Records.First().CryptoSendTxId);
        Assert.Equal("2021-06-04T14:52:41", response.Values.Records.First().Notified);
        Assert.Equal("2021-06-04T14:52:42", response.Values.Records.First().Traded);
        Assert.Equal("2021-06-04T14:52:43", response.Values.Records.First().Confirmed);
        Assert.Null(response.Values.Records.First().Finished);
        Assert.Equal("Exceeded maximum allowed transaction value set by DailySellLimit", response.Values.Records.First().Comment);
        Assert.Equal(30.0, response.Values.Records.First().CryptoAmount);
        Assert.Null(response.Values.Records.First().CryptoSent);
        Assert.Equal(30.0, response.Values.Records.First().CryptoTraded);
        Assert.Equal(0.312140214, response.Values.Records.First().CryptoExpectedAmount);
        Assert.Equal(0.312140214, response.Values.Records.First().CryptoEstimatePrice);
        Assert.Equal(0.312140214, response.Values.Records.First().CryptoTradePrice);
        Assert.Equal(0.312140214, response.Values.Records.First().CryptoPrice);
        Assert.Equal(9.36396, response.Values.Records.First().TradeValue);
        Assert.Equal(0.8214, response.Values.Records.First().BankCommission);
        Assert.Equal(0.1404594, response.Values.Records.First().PartnerCommission);
        Assert.Equal(0.1, response.Values.Records.First().NetworkCommission);
        Assert.Equal(8.402100599999999, response.Values.Records.First().Payout);
        Assert.Null(response.Values.Records.First().PayComment);
        Assert.Null(response.Values.Records.First().BankTransferReference);
        Assert.True(response.Values.Records.First().IsSettled);
    }

    [Fact]
    public void GetTransaction_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""created"": ""2021-06-04T14:52:41"",
                            ""transactionCode"": ""DT20210604145241SL6"",
                            ""transactionType"": ""SELL"",
                            ""status"": ""PayoutOnHold"",
                            ""type"": ""SELL"",
                            ""portfolioCode"": null,
                            ""customerCode"": ""NL51INGB7243913512"",
                            ""accountCode"": ""XYFWTUQF"",
                            ""cryptoCurrencyCode"": ""XLM"",
                            ""currencyCode"": ""EUR"",
                            ""enchangeCode"": ""XLMWALLETDUROTAN"",
                            ""paymentMethodCode"": ""DT_CRYPTO_SELL_XLM_EUR"",
                            ""exchangeOrderCode"": null,
                            ""cryptoSendTxId"": null,
                            ""cryptoReceiveTxId"": ""Hic dolor qui et quas unde enim."",
                            ""notified"": ""2021-06-04T14:52:41"",
                            ""traded"": ""2021-06-04T14:52:42"",
                            ""confirmed"": ""2021-06-04T14:52:43"",
                            ""finished"": null,
                            ""comment"": ""Exceeded maximum allowed transaction value set by DailySellLimit"",
                            ""cryptoAmount"": 30.0,
                            ""cryptoSent"": null,
                            ""cryptoTraded"": 30.0,
                            ""cryptoEstimatePrice"": 0.312140214,
                            ""cryptoExpectedAmount"": 0.312140214,
                            ""cryptoTradePrice"": 0.312140214,
                            ""cryptoPrice"": 0.312140214,
                            ""tradeValue"": 9.36396,
                            ""bankCommission"": 0.8214,
                            ""partnerCommission"": 0.1404594,
                            ""networkCommission"": 0.1,
                            ""payout"": 8.402100599999999,
                            ""payComment"": null,
                            ""bankTransferReference"": null,
                            ""isSettled"": true
                            }
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/transaction/DT20210604145241SL6"))
            {
                Headers = {
                    { "api_version", "1.2" }
                }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        var response = _logicHelper.ApiService.GetTransaction("DT20210604145241SL6").Result;

        Assert.Equal("Successfully processed your request", response.Message);
        Assert.Null(response.Errors);

        Assert.Equal("2021-06-04T14:52:41", response.Values.Created);
        Assert.Equal("DT20210604145241SL6", response.Values.TransactionCode);
        Assert.Equal("SELL", response.Values.TransactionType);
        Assert.Equal("PayoutOnHold", response.Values.Status);
        Assert.Equal("SELL", response.Values.Type);
        Assert.Null(response.Values.PortfolioCode);
        Assert.Equal("NL51INGB7243913512", response.Values.CustomerCode);
        Assert.Equal("XYFWTUQF", response.Values.AccountCode);
        Assert.Equal("XLM", response.Values.CryptoCurrencyCode);
        Assert.Equal("EUR", response.Values.CurrencyCode);
        Assert.Equal("XLMWALLETDUROTAN", response.Values.ExchangeCode);
        Assert.Equal("DT_CRYPTO_SELL_XLM_EUR", response.Values.PaymentMethodCode);
        Assert.Null(response.Values.ExchangeOrderCode);
        Assert.Null(response.Values.CryptoSendTxId);
        Assert.Equal("2021-06-04T14:52:41", response.Values.Notified);
        Assert.Equal("2021-06-04T14:52:42", response.Values.Traded);
        Assert.Equal("2021-06-04T14:52:43", response.Values.Confirmed);
        Assert.Null(response.Values.Finished);
        Assert.Equal("Exceeded maximum allowed transaction value set by DailySellLimit", response.Values.Comment);
        Assert.Equal(30.0, response.Values.CryptoAmount);
        Assert.Null(response.Values.CryptoSent);
        Assert.Equal(30.0, response.Values.CryptoTraded);
        Assert.Equal(0.312140214, response.Values.CryptoExpectedAmount);
        Assert.Equal(0.312140214, response.Values.CryptoEstimatePrice);
        Assert.Equal(0.312140214, response.Values.CryptoTradePrice);
        Assert.Equal(0.312140214, response.Values.CryptoPrice);
        Assert.Equal(9.36396, response.Values.TradeValue);
        Assert.Equal(0.8214, response.Values.BankCommission);
        Assert.Equal(0.1404594, response.Values.PartnerCommission);
        Assert.Equal(0.1, response.Values.NetworkCommission);
        Assert.Equal(8.402100599999999, response.Values.Payout);
        Assert.Null(response.Values.PayComment);
        Assert.Null(response.Values.BankTransferReference);
        Assert.True(response.Values.IsSettled);
    }

    [Fact]
    public void GetTransactionTotals_Success()
    {
        var mockResponseBody =
            @"
                    {
                        ""message"": ""Successfully processed your request"",
                        ""errors"": null,
                        ""values"": {
                            ""filteringParameters"": {},
                            ""totals"": [
                                {
                                    ""status"": ""BUYCOMPLETED"",
                                    ""currencyCode"": ""USD"",
                                    ""transactionCount"": 4,
                                    ""payoutValue"": 9.38,
                                    ""tradeValue"": 12.93,
                                    ""bankCommission"": 3.35,
                                    ""partnerCommission"": 0.19,
                                    ""networkCommission"": 0.00103
                                }
                            ]
                        }
                    }
                ";

        _logicHelper.MockResponseHandler.AddMockResponse(
            new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.quantoznexus.com/transaction/totals"))
            {
                Headers = {
                    { "api_version", "1.2" }
                }
            },
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseBody, Encoding.UTF8, "application/json"),
            });

        using (var client = _logicHelper.ApiClientFactory.GetClient(null))
        {
            var response = _logicHelper.ApiService.GetTransactionTotals(new System.Collections.Generic.Dictionary<string, string>()).Result;
            Assert.Equal("Successfully processed your request", response.Message);
            Assert.Null(response.Errors);

            Assert.Equal(0, response.Values.FilteringParameters.Count());

            var buyCompleted = response.Values.Totals.Single(x => x.Status == "BUYCOMPLETED");
            Assert.Equal("USD", buyCompleted.CurrencyCode);
            Assert.Equal(4, buyCompleted.TransactionCount);
            Assert.Equal(9.38, buyCompleted.PayoutValue);
            Assert.Equal(12.93, buyCompleted.TradeValue);
            Assert.Equal(3.35, buyCompleted.BankCommission);
            Assert.Equal(0.19, buyCompleted.PartnerCommission);
            Assert.Equal(0.00103, buyCompleted.NetworkCommission);
        }
    }

}
