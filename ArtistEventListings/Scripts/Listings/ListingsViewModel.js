var listingsViewModule = (function (listingsViewModule) {
    listingsViewModule.listingsViewModel = {
        eventId: ko.observable(),
        listings: ko.observableArray(),
        ticketTypes: ko.observableArray(["Any", "ETicket", "PaperTicket"]),
        selectedTicketType: ko.observable(),
        selectedTicketAmount: ko.observable(),
        selectedPage: ko.observable(1),
        pagedListings: ko.observableArray(),
        pageSize: ko.observable(16),
        pager: ko.pager(0, 0),
        setSelectedTicketAmount: function (numberOfTickets) {
            listingsViewModule.listingsViewModel.selectedTicketAmount(numberOfTickets);
            listingsViewModule.listingsViewModel.goToPage(1);
        },
        loading: ko.observable(true),
        goToPage: function (pageNum) {            
            var selectedTicketType = listingsViewModule.listingsViewModel.selectedTicketType(),
                pageNum = pageNum || 1,
                numberOfTickets = listingsViewModule.listingsViewModel.selectedTicketAmount();

            listingsViewModule.listingsViewModel.loading(true);
            listingsViewModule.listingsViewModel.selectedPage(pageNum);
            listingsViewModule.listingsViewModel.listings.removeAll();
            listingsViewModule.listingsViewModel.pagedListings.removeAll();

            if (listingsViewModule.listingsViewModel.selectedTicketType() === "Any") {
                listingsViewModule.listingsViewModel.getListings(numberOfTickets, pageNum);
            } else {
                listingsViewModule.listingsViewModel.getListingsByTicketType(numberOfTickets, selectedTicketType, pageNum);
            }
        },
        getListings: function (numberOfTickets, page) {
            $.ajax({
                url: window.root + "/Listings/GetListings",
                data: {
                    eventId: listingsViewModule.listingsViewModel.eventId(),
                    numberOfTickets: numberOfTickets,
                    page: page
                },
                success: function (data) {
                    listingsViewModule.listingsViewModel.setUpTable(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(errorThrown);
                }
            });
        },
        getListingsByTicketType: function (numberOfTickets, ticketType, page) {
            $.ajax({
                url: window.root + "/Listings/GetListingsByTicketType",
                data: {
                    eventId: listingsViewModule.listingsViewModel.eventId(),
                    numberOfTickets: numberOfTickets,
                    ticketType: ticketType,
                    page: page
                },
                success: function (data) {
                    listingsViewModule.listingsViewModel.setUpTable(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(errorThrown);
                }
            });
        },
        setUpTable: function (data) {
            listingsViewModule.listingsViewModel.listings(data.Listings);
            listingsViewModule.listingsViewModel.pagedListings().push({
                pager: ko.pager(data.TotalListings, listingsViewModule.listingsViewModel.pageSize())
            });
            listingsViewModule.listingsViewModel.pagedListings()[0].pager().CurrentPage(listingsViewModule.listingsViewModel.selectedPage());
            listingsViewModule.listingsViewModel.pagedListings.valueHasMutated();
            listingsViewModule.listingsViewModel.loading(false);
            $('.table').css({ "display": "table" });
            $('.pagination').css({ "display": "block" });
        }
    }

    var init = listingsViewModule.init;
    listingsViewModule.init = function () {
        listingsViewModule.listingsViewModel.ticketsSelectedText = ko.pureComputed(function () {
            var selectedTicketAmount = listingsViewModule.listingsViewModel.selectedTicketAmount();
            var numTicketsSelected = (selectedTicketAmount === undefined || selectedTicketAmount === null ? "Any Number of " : selectedTicketAmount);
            var ticketPlural = selectedTicketAmount != 1 ? " Tickets" : " Ticket";
            return numTicketsSelected + ticketPlural + " Selected";
        }, listingsViewModule.listingsViewModel);

        ko.applyBindings(listingsViewModule.listingsViewModel, document.getElementById("listingsContainer"));
        window.root = location.protocol + '//' + location.host;

        var link = window.location.href;
        var linkValues = new Array();
        linkValues = link.split("=");
        var eventId = unescape(linkValues[1]);

        listingsViewModule.listingsViewModel.eventId(eventId);
        listingsViewModule.listingsViewModel.getListings(null, 1);

        listingsViewModule.listingsViewModel.selectedTicketType.subscribe(function (newSelectedTicketType) {
            listingsViewModule.listingsViewModel.selectedTicketType(newSelectedTicketType);
            listingsViewModule.listingsViewModel.goToPage(1);
        });
        
        if (init) { init.apply(this, arguments); }
    }

    return listingsViewModule;
}(listingsViewModule || {}));