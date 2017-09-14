(function () {
    'use strict';
    var controllerId = 'symbol';
    angular.module('icodao').controller(controllerId, ['common', '$scope', 'dataservice', 'exchangeservice', '$stateParams', symbol]);

    function symbol(common, $scope, dataservice, exchangeservice, $stateParams) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var vm = this;
        vm.info = 'symbol';
        $scope.foo = $stateParams.symbol;
        vm.symbol = $stateParams.symbol;
        //dataservice.getData().then(function (data) {
        //    vm.nodes = data.nodes;
        //    vm.node = vm.nodes.filter(function (node) {
        //        return node.id === nodeId;
        //    })[0];
        //    vm.getTransactions();
        //});

        $scope.datayahoo = [];
        $scope.users = [1, 2, 3, 4];
        $scope.amount1 = [1000, 2100, 3330, 400];
        $scope.amount2 = [1100, 2200, 133000, 1400];
        vm.orders = [];
        $scope.ordersPage = 1;
        $scope.isEdit = false;

        //dataservice.getPrices().then(function (data) {
        //    if (data.status === 200)
        //        $scope.prices = data.data;
        //});
        setInterval(function updateRandom() {
            if (!$scope.isEdit)
                getData();
        }, 10000);


        var masteruser = {};
        $scope.edit = function (user) {
            masteruser = angular.copy(user);
            $scope.changeview(user);
        }
        $scope.save = function (user) {
            $scope.changeview(user);
        }
        $scope.cancel = function (user) {
            user.max = masteruser.max;
            user.min = masteruser.min;
            $scope.changeview(user);
        }
        $scope.changeview = function (user) {
            user.isEdit = !user.isEdit;
            $scope.isEdit = !$scope.isEdit;
        }
        function orderStack(symbol) {
            return exchangeservice.orderStack(symbol).then(function (data) {
                vm.orderStack = data.data;
                vm.bidOrders = vm.orderStack.bidOrders;
                vm.askOrders = vm.orderStack.askOrders;
            });
        }
        function symbolOrderCommands(symbol) {
            var start = 1;
            var pageSize = 50;
            return exchangeservice.symbolOrderCommands(symbol, start, pageSize).then(function (data) {
                vm.symbolOrderCommands = data.data;
            });
        }
        function symbolOrderEvents(symbol) {
            var start = 1;
            var pageSize = 50;
            return exchangeservice.symbolOrderEvents(symbol, start, pageSize).then(function (data) {
                vm.symbolOrderEvents = data.data;
            });
        }
        function getOrders() {
            return exchangeservice.getOrders().then(function (data) {
                vm.orders = Object.values(data.data);
            });
        }
        function getSymbol() {
            return exchangeservice.mainSymbol().then(function (data) {

                vm.symbol = data.data;
            });
        }
        function getSymbols() {
            return exchangeservice.symbols().then(function (data) {

                vm.symbols = data.data;
            });
        }
        function symbolOrderCommandsCount(symbol) {
            return exchangeservice.symbolOrderCommandsCount(symbol).then(function (data) {
                vm.symbolOrderCommandsCount = data.data;
            });
        }

        function symbolOrderEventsCount(symbol) {
            return exchangeservice.symbolOrderEventsCount(symbol).then(function (data) {
                vm.orderEventsCount = data.data;
            });
        }

        function symbolFullOrdersCount(symbol) {
            return exchangeservice.symbolFullOrdersCount(symbol).then(function (data) {
                vm.fullOrdersCount = data.data;
            });
        }
        activate();
        function activate() {
            common.activateController([getData()], controllerId)
                .then(function () { log('Activated symbol') });//log('Activated Admin View');
        }


        function getData() {
            orderStack(vm.symbol);
            symbolOrderCommands(vm.symbol);
            symbolOrderEvents(vm.symbol);
            //getOrders();
            //getSymbol();
            //getSymbols();
            symbolOrderCommandsCount(vm.symbol);
            symbolOrderEventsCount(vm.symbol);
            symbolFullOrdersCount(vm.symbol);
        }
    };




})();