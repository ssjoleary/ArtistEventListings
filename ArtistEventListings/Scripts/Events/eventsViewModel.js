var eventsViewModule = (function (eventsViewModule) {
    eventsViewModule.eventsViewModel = {
        events: ko.observableArray(),
        pagedEvents: ko.observableArray(),
        pageSize: ko.observable(15),
        goToPage: function (pageNum) {
            eventsViewModule.eventsViewModel.pagedEvents.removeAll();
            var allEvents = eventsViewModule.eventsViewModel.events(),
                pageSize = eventsViewModule.eventsViewModel.pageSize(),
                startingIndex = (pageSize * (pageNum - 1)),
                endIndex = startingIndex + pageSize,
                newEvents = allEvents.slice(startingIndex, endIndex);

            eventsViewModule.eventsViewModel.pagedEvents().push({
                events: ko.observableArray(newEvents),
                pager: ko.pager(allEvents.length, pageSize)
            });
            eventsViewModule.eventsViewModel.pagedEvents()[0].pager().CurrentPage(pageNum);
            eventsViewModule.eventsViewModel.pagedEvents.valueHasMutated();
            $('.pagination').css({ "display": "block" });
        },
        viewTickets: function (data) {
            var url = window.root + '/listings?eventId=' + data.Id
            window.location = url;
        },
        loading: ko.observable(true)
    }    
    
    var init = eventsViewModule.init;
    eventsViewModule.init = function () {
        ko.applyBindings(eventsViewModule.eventsViewModel, document.getElementById("eventsContainer"));
        window.root = location.protocol + '//' + location.host;;
        $.ajax({
            url: window.root + "/Events/GetEventsWithCheapestHighlighted",
            success: function (data) {
                $.each(data, function (index, value) {
                    value.StartDate = eval("new " + value.StartDate.slice(1, -1));
                });
                eventsViewModule.eventsViewModel.events(data);
                eventsViewModule.eventsViewModel.goToPage(1);
                eventsViewModule.eventsViewModel.loading(false);
                $('.table').css({ "display": "table" });
                $('.pagination').css({ "display": "block" });
            }
        });

        if (init) { init.apply(this, arguments); }
    }

    return eventsViewModule;
}(eventsViewModule || {}));