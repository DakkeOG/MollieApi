﻿using Mollie.Api.Client;
using Mollie.Api.Models.List;
using Mollie.Api.Models.Payment;
using Mollie.Api.Models.PaymentMethod;
using Mollie.Api.Models.Profile.Response;
using Mollie.Tests.Integration.Framework;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Mollie.Tests.Integration.Api {
    [TestFixture]
    public class ProfileTests : BaseMollieApiTestClass {
        [Test]
        public async Task GetCurrentProfileAsync_ReturnsCurrentProfile() {
            // Given

            // When: We retrieve the current profile from the mollie API
            ProfileResponse profileResponse = await this._profileClient.GetCurrentProfileAsync();

            // Then: Make sure we get a valid response
            Assert.IsNotNull(profileResponse);
            Assert.IsNotNull(profileResponse.Id);
            Assert.IsNotNull(profileResponse.Email);
            Assert.IsNotNull(profileResponse.Status);
        }

        [Test]
        public async Task EnablePaymentMethodAsync_WhenEnablingPaymentMethodForCurrentProfile_PaymentMethodIsReturned() {
            // Given

            // When: We enable a payment method for the current profile
            PaymentMethodResponse paymentMethodResponse = await this._profileClient.EnablePaymentMethodAsync(PaymentMethod.Ideal);

            // Then: Make sure a payment method is returned
            Assert.IsNotNull(paymentMethodResponse);
            Assert.AreEqual(PaymentMethod.Ideal, paymentMethodResponse.Id);
        }

        [Test]
        [Ignore("We can only test this in debug mode, because we need to retrieve a oauth access token to test this method")]
        public async Task EnablePaymentMethodAsync_WhenEnablingPaymentMethodForProfile_PaymentMethodIsReturned() {
            // Given: We retrieve the profile from the API
            ProfileClient profileClient = new ProfileClient("abcde"); // Set access token
            ListResponse<ProfileResponse> allProfiles = await profileClient.GetProfileListAsync();
            if (allProfiles.Items.Count == 0) {
                Assert.Inconclusive("No profiles found. Unable to continue test");
            }
            ProfileResponse profileToTestWith = allProfiles.Items.First();
            

            // When: We enable a payment method for the given profile
            PaymentMethodResponse paymentMethodResponse = await profileClient.EnablePaymentMethodAsync(profileToTestWith.Id, PaymentMethod.Ideal);

            // Then: Make sure a payment method is returned
            Assert.IsNotNull(paymentMethodResponse);
            Assert.AreEqual(PaymentMethod.Ideal, paymentMethodResponse.Id);
        }
    }
}