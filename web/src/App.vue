<template>
  <div id="app" class="container pt-5">
    <div class="row justify-content-center">
      <div class="col-8">
        <new-applicant v-if="userFlow === 0" @submit="onSubmitApplicant" />
        <new-mortgages
          v-else-if="userFlow === 1"
          @submit="getQualifyingMortgages"
        />
        <qualifying-mortgages v-else :mortgages="mortgages" @reset="onReset" />
      </div>
    </div>
  </div>
</template>

<script>
import NewApplicant from "./components/NewApplicant";
import QualifyingMortgages from "./components/QualifyingMortgages";
import NewMortgages from "./components/NewMortgages";
import axios from "axios";

export default {
  name: "App",
  components: {
    NewApplicant,
    NewMortgages,
    QualifyingMortgages
  },
  data() {
    return {
      applicant: {
        id: "",
        firstName: "",
        lastName: "",
        dateOfBirth: null,
        email: ""
      },
      mortgages: [],
      userFlow: 0
    };
  },
  methods: {
    async onSubmitApplicant(applicantDetails) {
      try {
        for (let key in applicantDetails) {
          this.applicant[key] = applicantDetails[key];
        }
        let response = await axios.post("/api/applicants", applicantDetails);
        this.applicant.id = response.data.id;
        this.userFlow = 1;
      } catch (e) {
        this.applicant = {
          id: "",
          firstName: "",
          lastName: "",
          dateOfBirth: null,
          email: ""
        };
        console.error(e);
      }
    },
    async getQualifyingMortgages(model) {
      try {
        let response = await axios.get(
          `/api/mortgages?applicantId=${this.applicant.id}&propertyValue=${model.propertyValue}&depositValue=${model.depositValue}`
        );
        this.mortgages = response.data;
        this.userFlow = 2;
      } catch (e) {
        this.mortgages = [];
        console.log(e);
      }
    },
    onReset() {
      this.applicant = {
        id: "",
        firstName: "",
        lastName: "",
        dateOfBirth: null,
        email: ""
      };
      this.mortgages = [];
      this.userFlow = 0;
    }
  }
};
</script>

<style lang="scss">
@import "~bootstrap/scss/bootstrap";
</style>
