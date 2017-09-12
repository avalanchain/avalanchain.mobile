/**
 * icodao - Responsive Admin Theme
 * Copyright 2015 Webapplayers.com
 *
 */
(function () {
    var app = angular.module('icodao', [
        'ui.router',                    // Routing
        'oc.lazyLoad',                  // ocLazyLoad
        'ui.bootstrap',                 // Ui Bootstrap
        'common',
        'monospaced.qrcode',
        'ncy-angular-breadcrumb',
        'irontec.simpleChat',
        'luegg.directives'
        // 'localytics.directives'
        // 'AdalAngular'
    ]);


    app.run(['$templateCache', '$rootScope', '$state', '$stateParams', 'dataservice', function ($templateCache, $rootScope, $state, $stateParams, dataservice) {
       $rootScope.$state = $state;
       dataservice.getData().then(function (data) {
        $rootScope.mdata = data;
        $rootScope.search = $rootScope.mdata.accounts;
       });
    //    $rootScope.showAccount = function(account) {
    //        $state.go('index.account', {
    //            accountId: account.ref.address
    //        });
    //    }

    $rootScope.onSelect = function ($item, $model, $label) {
        $rootScope.$item = $item;
        // $scope.$model = $model;
        // $scope.$label = $label;
        $state.go('index.account', {
            accountId: $rootScope.$item.ref.address
        });
    };
    }]);


})();
