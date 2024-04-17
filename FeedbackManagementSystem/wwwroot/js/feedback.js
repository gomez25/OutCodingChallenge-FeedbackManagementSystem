class FeedbackForm {
    constructor() {
        this.customerNameInput = $("#customerName");
        this.descriptionInput = $("#description");
        this.categoryIdInput = $("#categoryId");
        this.feedbackForm = $("#feedbackForm");
        this.feedbackResult = $("#feedbackResult");
        this.submitBtn = $("#submitBtn");
    }

    validateForm() {
        var isValid = true;

        if (this.customerNameInput.val() == "") {
            isValid = false;
            this.customerNameInput.addClass("is-invalid");
        } else {
            this.customerNameInput.removeClass("is-invalid");
        }

        if (this.descriptionInput.val() == "") {
            isValid = false;
            this.descriptionInput.addClass("is-invalid");
        } else {
            this.descriptionInput.removeClass("is-invalid");
        }

        if (this.categoryIdInput.val() == "" || this.categoryIdInput.val() == "Select Category") {
            isValid = false;
            this.categoryIdInput.addClass("is-invalid");
        } else {
            this.categoryIdInput.removeClass("is-invalid");
        }

        return isValid;
    }

    submitForm(successMessage) {
        var _this = this;

        $.ajax({
            url: this.feedbackForm.attr("action"),
            type: this.feedbackForm.attr("method"),
            data: this.feedbackForm.serialize(),
            success: function (result) {
                _this.addBadge(successMessage);
            },
            error: function () {
                _this.feedbackResult.html("An error occurred while saving feedback.");
            }
        });
    }

    addBadge(successMessage) {
        this.feedbackForm.next(".badge").remove();
        var badge = $("<span class='badge badge-success'>" + successMessage + "</span>");
        this.feedbackForm.after(badge);
    }
}