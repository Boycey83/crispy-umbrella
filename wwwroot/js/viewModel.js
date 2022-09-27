ab.beerQuest.viewModel = function () {
    var self = this;

    // Results
    self.venues = ko.observable();

    // Filters - Selected Values
    self.selectedCategories = ko.observableArray();
    self.beerRating = ko.observable(0);
    self.atmosphereRating = ko.observable(0);
    self.amenitiesRating = ko.observable(0);
    self.valueRating = ko.observable(0);
    self.selectedTags = ko.observableArray();
    self.sortOrder = ko.observable();

    self.sortOrder.subscribe(function() {
        console.log(self.sortOrder());
    });

    // Filter Options
    self.allAvailableTags = ko.observable();
    self.categoryOptions = ko.observableArray([
        { value: ab.beerQuest.enums.category.barReviews, text: "Bar Reviews" },
        { value: ab.beerQuest.enums.category.pubReviews, text: "Pub Reviews" },
        { value: ab.beerQuest.enums.category.otherReviews, text: "Other Reviews" },
        { value: ab.beerQuest.enums.category.closedVenues, text: "Closed Venues" },
        { value: ab.beerQuest.enums.category.uncategorized, text: "Uncategorized" }
    ]);

    self.ratingOptions = ko.observableArray([
        { value: 0, text: "Any" },
        { value: 1, text: "1+" },
        { value: 2, text: "2+" },
        { value: 3, text: "3+" },
        { value: 4, text: "4+" },
        { value: 5, text: "5" }
    ]);

    self.sortOrderOptions = ko.observableArray([
        { value: ab.beerQuest.enums.sortOrder.newest, text: "Newest" },
        { value: ab.beerQuest.enums.sortOrder.starsBeer, text: "Beer Rating" },
        { value: ab.beerQuest.enums.sortOrder.starsAtmosphere, text: "Atmosphere Rating" },
        { value: ab.beerQuest.enums.sortOrder.starsAmenities, text: "Amenities Rating" },
        { value: ab.beerQuest.enums.sortOrder.starsValue, text: "Value Rating" }
    ]);

    // Service Functions
    self.loadVenues = function (sortOrder) {
        var getVenuesUrl = sortOrder === undefined ? "/venues" : "/venues?sortOrder="+sortOrder;
        $.get(getVenuesUrl)
            .done(function (venueData) {
                self.venues(ko.utils.arrayMap(venueData, function (v) {
                    return new ab.beerQuest.venue(v);
                }));
            });
    };

    self.loadTags = function (sortOrder) {
        $.get("/venues/tags")
            .done(function (tagData) {
                self.allAvailableTags(tagData);
            });
    };

    self.filterVenues = function (data) {
        $.ajax({
            type: "POST",
            url: "/venues/filter",
            data: ko.toJSON(data),
            dataType : "json",
            contentType : "application/json",
        })
            .done(function (venueData) {
                self.venues(ko.utils.arrayMap(venueData, function (v) {
                    return new ab.beerQuest.venue(v);
                }));
            });
    };

    self.loadTags();

    // Loader Computed
    ko.computed(function () {
        var filters = {};
        // Build Filters
        if (self.selectedCategories() && self.selectedCategories().length > 0) {
            filters.categories = self.selectedCategories();
        }
        if (self.beerRating()) {
            filters.starsBeer = self.beerRating();
        }
        if (self.atmosphereRating()) {
            filters.starsAtmosphere = self.atmosphereRating();
        }
        if (self.amenitiesRating()) {
            filters.starsAmenities = self.amenitiesRating();
        }
        if (self.valueRating()) {
            filters.starsValue = self.valueRating();
        }
        if (self.selectedTags() && self.selectedTags().length > 0) {
            filters.tags = self.selectedTags();
        }

        // Load data
        if (Object.keys(filters).length === 0) {
            self.loadVenues(self.sortOrder())
        } else {
            if (self.sortOrder()) {
                filters.sortOrder = self.sortOrder();
            }
            self.filterVenues(filters);
        }
    })
}