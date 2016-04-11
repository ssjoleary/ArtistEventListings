(function (ko) {
    var numericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.computed({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                $.publish("Pager/BeginPage");
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
                $.publish("Pager/EndPage");
            }
        });

        return result;
    };

    function Pager(totalItemCount, pageSize) {
        var self = this;
        self.CurrentPage = numericObservable(1);
        self.TotalItemCount = ko.observable(totalItemCount);
        self.PageSize = numericObservable(pageSize);
        self.PageSlide = numericObservable(2);

        self.LastPage = ko.computed(function () {
            return Math.ceil(self.TotalItemCount() / self.PageSize());
        });

        self.HasNextPage = ko.computed(function () {
            return self.CurrentPage() < self.LastPage();
        });

        self.HasPrevPage = ko.computed(function () {
            return self.CurrentPage() > 1;
        });

        self.FirstItemIndex = ko.computed(function () {
            return self.PageSize() * (self.CurrentPage() - 1) + 1;
        });

        self.LastItemIndex = ko.computed(function () {
            return Math.min(self.FirstItemIndex() + self.PageSize() - 1, self.TotalItemCount());
        });

        self.ThisPageCount = ko.computed(function () {
            var mod = self.LastItemIndex() % self.PageSize();
            if (mod > 0) return mod;
            return self.PageSize();
        });

        self.Pages = ko.computed(function () {
            var pageCount = self.LastPage();
            var pageFrom = Math.max(1, self.CurrentPage() - self.PageSlide());
            var pageTo = Math.min(pageCount, self.CurrentPage() + self.PageSlide());
            pageFrom = Math.max(1, Math.min(pageTo - 2 * self.PageSlide(), pageFrom));
            pageTo = Math.min(pageCount, Math.max(pageFrom + 2 * self.PageSlide(), pageTo));

            var result = [];
            for (var i = pageFrom; i <= pageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.pager = function (totalItemCount, pageSize) {
        var pager = new Pager(totalItemCount, pageSize);
        return ko.observable(pager);
    };
}(ko));