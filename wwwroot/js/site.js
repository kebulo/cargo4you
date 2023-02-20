let app = new Vue({
    el: '#cuotationModal',
    data: {
        rules: [],
        quotation: {},
        shipmentData: {},
        volume: 0,
        quotationId: '',
        request_shippment: false,
        price_details: {
            show_price: false
        },
        errorMessage: "",
        shippmentSuccess: false
    },
    methods: {
        /**
         * Get cuotation price base on rules and inputs by the customer
         * Store results in VueJS variables state to be shown on the page.
        */
        getQuotation() {
            if (!(this.quotation.width && this.quotation.height && this.quotation.depth) && !this.quotation.weight) {
                this.errorMessage = "You must filled all the dimension fields or the weight of the article to proceed with the quotation";
                return null;
            }

            if (this.quotation.width && this.quotation.height && this.quotation.depth) {
                this.volume = this.quotation.width * this.quotation.height * this.quotation.depth;
            } else {
                this.volume = 0;
            }

            let [total_quotation, rule] = this.calculatePrice();

            this.price_details = {
                price: total_quotation,
                company: rule.courierName,
                show_price: true
            };

            let data = {
                "Price": total_quotation,
                "CourierId": rule.courierId
            };

            if (this.quotation.name)
                data.ClientName = this.quotation.name;
            if (this.quotation.width)
                data.Width = this.quotation.width;
            if (this.quotation.height)
                data.Height = this.quotation.height;
            if (this.quotation.depth)
                data.Depth = this.quotation.depth;
            if (this.quotation.weight)
                data.Weight = this.quotation.weight;

            fetch('/api/Quotations/', {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify(data)
            })
            .then((resp) => resp.json())
            .then((json) => {
                this.quotationId = json;
            }).catch(error => console.error(error));
        },
        /**
         * Get client data and save the shipment data
         * Store results in VueJS variables state to be shown on the page.
        */
        saveShippment() {
            if (!(this.shipmentData.ClientName && this.shipmentData.Address && this.shipmentData.PhoneNumber)) {
                this.errorMessage = "You must complete the data before submitting the form";

                return null;
            }

            this.shipmentData.shipmentConfirm = true;

            fetch('/api/Quotations/' + this.quotationId, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "PUT",
                body: JSON.stringify(this.shipmentData)
            })
            .then((resp) => resp.json())
            .then((json) => {
                this.shippmentSuccess = true;
            })
            .catch(error => console.error(error));
            
        },
        /**
         * Calculate price based on rules
         * @return {Array} temp_price and temp_rule calculated.
        */
        calculatePrice() {
            let temp_rule;
            let temp_price = 0;

            for (let i = 0; i < this.rules.length-1; i++) {
                let item = this.rules[i];

                let max = (item.max != '+') ? parseInt(item.max) : item.max;
                let min = parseInt(item.min);

                if (typeof max == "number") {
                    // Dimension calculation
                    if (item.isDimension && this.volume > 0 && this.volume > min && this.volume <= max) {
                        if (item.price > temp_price) {
                            temp_rule = item;
                            temp_price = item.price
                        }
                    }
                    // Weight calculation
                    if (!item.isDimension && this.quotation.weight > 0 && this.quotation.weight > min && this.quotation.weight <= max) {
                        if (item.price > temp_price) {
                            temp_rule = item;
                            temp_price = item.price;
                        }
                    }
                } else {
                    // Dimension max calculation
                    if (item.isDimension && this.volume > 0 && this.volume >= min) {
                        if (item.price > temp_price) {
                            temp_rule = item
                            temp_price = item.price
                        }
                    }
                    // Weight max calculation
                    if (!item.isDimension && this.quotation.weight > 0 && this.quotation.weight >= min) {
                        let extra_price = (((item.extraKg - this.quotation.weight) * -1) * item.extraValue) + item.price;

                        if (extra_price > temp_price) {
                            temp_rule = item;
                            temp_price = extra_price;
                        }
                    }
                }
            }

            return [temp_price, temp_rule];
        },
        /**
         * Close modal
         * It validates that the bootstrap modal was closed and then restore all the forms and status
        */
        closeModal() {
            this.quotation = {};
            this.shipmentData = {};
            this.volume = 0;
            this.quotationId = ''; 
            this.request_shippment = false;
            this.price_details = { show_price: false };
            this.errorMessage = "";
            this.shippmentSuccess = false;
        }
    },
    created: function() {
        fetch('/api/Rules/')
            .then(res => res.json())
            .then(json => this.rules = json) 
    },
});