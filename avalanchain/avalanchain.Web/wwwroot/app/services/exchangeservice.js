
(function () {
    'use strict';

    angular
        .module('icodao')
        .factory('exchangeservice', exchangeservice);

    exchangeservice.$inject = ['$http', '$q', 'common', 'dataProvider', '$filter', '$timeout'];
    /* @ngInject */


    function exchangeservice($http, $q, common, dataProvider, $filter, $timeout) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn('exchangeservice');
        var data = {};
        var service = {
            getData: getData,
            getId: getId,
        };

        return service;

        function getData() {
            var defer = $q.defer();
            //data.clusters = data.clusters ? data.clusters : getClusters();
            //data.nodes = data.nodes ? data.nodes : getNodes();
            //data.accounts = data.accounts ? data.accounts : getAccounts();
            //data.streams = data.streams ? data.streams : getStreams();
            //data.transactions = data.transactions ? data.transactions : getTransactions();
            //data.chat = data.chat ? data.chat : getChat();
            //data.assets = data.assets ? data.assets : getAssets();

            $timeout(function () {
                defer.resolve(data);
            }, 200)

            return defer.promise;
        }
        function getId() {
            return common.createGuid().replace(/-/gi, '')
        }
    }
})();
