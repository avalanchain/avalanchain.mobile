﻿
(function() {
    'use strict';

    angular.module('smartmoney').filter('pagination', function() {
        return function(input, start) {
            if (input) {
                start = +start; //parse to int
                return input.slice(start);
            }
            return [];
        }
    });
    angular.module('smartmoney').filter('reverse', function() {
        return function(items) {
            return items.slice().reverse();
        };
    });

    angular
        .module('smartmoney')
        .factory('dataservice', dataservice);

    dataservice.$inject = ['$http', '$q', 'common', 'dataProvider', '$filter', '$timeout', 'exchangeservice'];
    /* @ngInject */


    function dataservice($http, $q, common, dataProvider, $filter, $timeout, exchangeservice) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn('dataservice');
        var data = {};
        var service = {
            getData: getData,
            sendPayment: sendPayment,
            getTransactions: getTransactions,
            newAccount: newAccount,
            getAccounts: getAccounts,
            getYData: getYData,
            addCluster: addCluster,
            addNode: addNode,
            addStream: addStream,
            deleteCluster: deleteCluster,
            clearAllProcesses: clearAllProcesses,
            getQuoka: getQuoka,
            commondata: commondata,
            getId : getId,
            getChat : getChat,
            getPrices: getPrices,
            mapping: mapping,
            addAsset: addAsset,
            getAssets: getAssets,
        };

        return service;

        function getChat() {
            var accounts = [];
            var sc = {};
            var chat = {};
            chat.messages = [];
            chat.users = [];
            chat.lastMessage = new Date();
            chat.users.push({
                id: getId(),
                name: 'you'
            });
            chat.users.push({
                id: getId(),
                name: 'server'
            });
            var names = ['you', 'server'];
            var sides = ['right', 'left'];
            var testMessages = ['Server is on', 'cluster created', 'admin subscribed to the server: '+ chat.users[1].id];

            for (var i = 0; i < 3; i++) {
                chat.messages.push({
                    id: getId(),
                    name: chat.users[1].name,
                    date: new Date(),
                    message: testMessages[i],
                    side: sides[1],

                })
                chat.lastMessage = new Date();
            }

            return chat;
            // return dataProvider.get(sc, '/api/account/all', function (data, status) {
            //     //$scope.GetAllProgresses = data;
            // });
        }

        function getAccounts() {
            var accounts = [];
            var sc = {};
            if (accounts.length < 1) {
                for (var i = 0; i < 200; i++) {
                    accounts.push({
                        name: getId(),
                        publicKey: getId(),
                        balance: Math.floor(Math.random() * 1000) + 100,
                        status: Math.floor(Math.random() * 3) + 1,
                        signed: true,
                        ref: {
                            address: getId()
                        }
                    })
                }
            }

            return accounts;
            // return dataProvider.get(sc, '/api/account/all', function (data, status) {
            //     //$scope.GetAllProgresses = data;
            // });
        }

        function sendPayment(payment) {
            return $http.post('/api/transaction/submit', payment)
                .success(function(data, status, headers, config) {
                    log("Transaction submited!"); //'" + JSON.stringify(data) + "'
                    return data;
                })
                .error(function(data, status, headers, config) {
                    var err = status + ", " + data;
                    log("Request failed: " + err);
                    //$scope.result = "Request failed: " + err;
                    return "error";
                });
        }


        function getTransactions() {
            var sc = {};
            var tusers = ["EUR/USD", "USD/EUR", "USD/JPY", "USD/GBP", "USD/AUD", "USD/CHF", "USD/SEK", "USD/NOK", "USD/RUB"];
            var tsystems = ["Error", "Warning", "Success"];
            var ausers = ["send", "receive", "denied", "accept"];
            var asystems = ["signed", "new cluster", "new node", "new account"];
            var types = ["users", "system"]
            var transactions = [];
            var account = '';
            for (var i = 1; i <= 1000; i++) {
                var type = (i % 2) == 0 ? types[0] : types[1];
                var node = i <= (1000/2) ? 0 : 1;
                var action, value, typename = '';
                if (type === "users") {
                    account = data.accounts[Math.floor(Math.random() * data.accounts.length)].ref.address;
                    action = ausers[Math.floor(Math.random() * ausers.length)];
                    typename = tusers[Math.floor(Math.random() * tusers.length)];
                } else {
                    account = 'system';
                    action = asystems[Math.floor(Math.random() * asystems.length)];
                    typename = tsystems[Math.floor(Math.random() * tsystems.length)];
                }
                transactions.push({
                    id: getId(),
                    publicKey: getId(),
                    node: data.nodes[node].id,
                    action: action,
                    account: account,
                    type: type,
                    typename: typename,
                    date: new Date()
                })
            }
            return transactions;
        }

        function getStreams() {
            var typenames = ["EUR/USD", "USD/EUR", "USD/JPY", "USD/GBP", "USD/AUD", "USD/CHF", "USD/SEK", "USD/NOK", "USD/RUB"];
            var types = ["user", "transaction"]
            var streams = [];
            for (var i = 1; i <= 200; i++) {
                var type = (i % 2) == 0 ? types[0] : types[1];
                var typename = Math.floor(Math.random() * typenames.length);
                streams.push({
                    id: getId(),
                    publicKey: data.nodes[i % 2].publicKey,
                    node: data.nodes[i % 2].id,
                    type: type,
                    typename: typenames[typename],
                    date: new Date()
                })
            }
            return streams
        }

        function getNodes() {
            var nodes = [];
            for (var i = 1; i <= 2; i++) {
                nodes.push({
                    id: getId(),
                    name:'ND-'+ i,
                    publicKey: getId(),
                    cluster: data.clusters[0].id
                })
            }
            return nodes;
        }

        function getClusters() {
            var clusters = [];
            for (var i = 1; i <= 1; i++) {
                clusters.push({
                    id: getId(),
                    name:'CL-'+ i,
                    publicKey: getId(),
                })
            }
            return clusters;
        }


        function getYData() {
            var url = "https://query.yahooapis.com/v1/public/yql";
            //var symbol = '"EURUSD","USDEUR", "USDJPY", "USDGBP", "USDAUD", "USDCHF", "USDSEK", "USDNOK", "USDRUB", "USDTRY", "USDBRL", "USDCAD", "USDCNY", "USDHKD", "USDINR", "USDKRW", "USDMXN", "USDNZD", "USDSGD", "USDZAR"';
            var symbol = '"EURUSD","USDEUR", "USDJPY", "USDGBP"';
            var data = encodeURIComponent("select * from yahoo.finance.xchange where pair in (symbol)");
            data = data.replace("symbol", symbol);
            /*
            http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20('aapl')&format=json&diagnostics=true&env=http://datatables.org/alltables.env
            */
            var str1 = url.concat("?q=", data);
            str1 = str1.concat("&format=json&env=store://datatables.org/alltableswithkeys"); //http://datatables.org/alltables.env

            var sc = {};


            return dataProvider.get(sc, str1, function(data, status) {
                //$scope.GetAllProgresses = data;
            });

        }
        function getPrices(currency) {
            if(!currency)
                currency='USD'

            var url = "https://min-api.cryptocompare.com/data/price?fsym=" + currency + "&tsyms=BTC,ETH,EUR,LTC";
            
            var sc = {};


            return dataProvider.get(sc, url, function(data, status) {
                //$scope.GetAllProgresses = data;
            });

        }

        function mapping(data, $scope){
            for (var i = 0; i < data.length; i++) {
                var from = mapvalues(data[i].Name.split('/')[1]);
                var to = mapvalues(data[i].Name.split('/')[0]);
                data[i].Name = from + '/' + to;
                data[i].id = from +  to;
                if($scope.prices && from!='USD'){
                    var delta = (data[i].Ask - data[i].Bid).toFixed(7);
                    data[i].Ask = Number(1/$scope.prices[from]) + Number(delta);
                    data[i].Bid = Number(1/$scope.prices[from]) - Number(delta);
                    data[i].Ask = (data[i].Ask).toFixed(2);
                    data[i].Bid = (data[i].Bid).toFixed(2);
                }else{
                    var delta = ((data[i].Ask - data[i].Bid)/100).toFixed(7);
                    data[i].Ask = Number($scope.prices[to]) + Number(delta);
                    data[i].Bid = Number($scope.prices[to]) - Number(delta);
                    data[i].Ask = (data[i].Ask).toFixed(7);
                    data[i].Bid = (data[i].Bid).toFixed(7);
                }
                
            }
            return data;
        }

        function mapvalues(value){
            var val = value;
            // str = str.replace(/abc/g, '');
            switch (value) {
                case 'JPY':
                val = 'LTC';
                  break;
                case "EUR":
                val = 'BTC';
                  break;
                case 'GBP':
                val = 'ETH';
                  break;
                // default:
                //   alert( 'Я таких значений не знаю' );
              }
              return val;
        }
        function addAsset(token) {
            data.assets.push({
                id: getId(),
                name: token.name,
                description: token.description,
                icon:'',
                amount: token.amount,
                decimals:token.decimals,
                reissuable:token.reissuable,
                status:"ICO coming",
                cluster: data.clusters[0].id,
                completion: 5
            })
        }

        function getAssets() {
            
            var assets = [];
            exchangeservice.symbols().then(function (data) {
                var symbols = data.data;
                
                for (var i = 0; i < symbols.length; i++) {

                    if (symbols[i] === 'AVC') {
                        assets.push({
                            id: getId(),
                            name: symbols[i],
                            description: 'CASCADING REACTIVE BLOCKCHAINS',
                            icon: '/assets/img/logo.png',
                            amount: 25000000,
                            decimals: 8,
                            reissuable: true,
                            status: "ICO coming",
                            //cluster: data.clusters[0].id,
                            completion: 48
                        });
                    } else {
                        if (symbols[i] === 'EUR' || symbols[i] === 'USD' || symbols[i] === 'GBP') {

                        } else {
                            assets.push({
                                id: getId(),
                                name: symbols[i],
                                description: (i % 2) == 0 ? 'Blockchain for Banks' : 'Blockchain Wallet',
                                icon: '',
                                amount: i * 1000000,
                                decimals: 8,
                                reissuable: (i % 2) == 0,
                                status: "ICO coming",
                                //cluster: data.clusters[0].id,
                                completion: 5 + i
                            });
                        }
                    }
                    
                }
            });
            
            return assets;
        }

        function addCluster() {
            
                    }
        function addNode() {

        }

        function addStream() {

        }

        function deleteCluster() {

        }

        function clearAllProcesses() {

        }

        function newAccount() {
          log("Account created!");
            // return $http.post('/api/account/new')
            //     .success(function(data, status, headers, config) {
            //         log("Account created!");
            //         return data;
            //     })
            //     .error(function(data, status, headers, config) {
            //         var err = status + ", " + data;
            //         log("Request failed: " + err);
            //         return "error";
            //     });
        }

        function getQuoka(datayahoo) {
            var data = datayahoo;
            var quoka = 0;
            if (data) {
                for (var i = 0; i < commondata().currencies().length; i++) {

                    if (i === 0) quoka += 0.45;
                    else {
                        quoka += (1 / data[i].Rate) * commondata().percentage()[i];
                    }

                }
                quoka = Math.round(quoka * 1000) / 1000;
            }
            return quoka;
        }

        function commondata() {
            var curr = ["USD", "EUR", "JPY", "GBP", "AUD", "CHF", "SEK", "NOK", "RUB", "TRY", "BRL", "CAD", "CNY", "HKD", "INR", "KRW", "MXN", "NZD", "SGD", "ZAR"];
            var percentage = [45, 17, 12, 6, 4, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5, 0.5];
            //return this.currencies = function () {

            //     return curr;
            // }
            //return this.percentage = function () {

            //     return percentage;
            //}

            return {
                currencies: function() {
                    return curr;
                },
                percentage: function() {
                    return percentage;
                }
            };
        }
        //  var data = {};

        function getData() {
            var defer = $q.defer();
            data.clusters = data.clusters ? data.clusters : getClusters();
            data.nodes = data.nodes ? data.nodes : getNodes();
            data.accounts = data.accounts ? data.accounts : getAccounts();
            data.streams = data.streams ? data.streams : getStreams();
            data.transactions = data.transactions ? data.transactions : getTransactions();
            data.chat = data.chat ? data.chat : getChat();
            data.assets = data.assets ? data.assets : getAssets();

            $timeout(function() {
                defer.resolve(data);
            }, 200)

            return defer.promise;
        }
        function getId() {
            return common.createGuid().replace(/-/gi, '')
        }
    }
})();
