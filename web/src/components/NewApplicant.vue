<template>
  <div class="card">
    <div class="card-body">
      <h2 class="card-title">New Applicant</h2>
      <form @submit.prevent="onSubmit">
        <div class="form-group">
          <label>
            First name
            <input type="text" class="form-control" v-model="model.firstName" />
          </label>
        </div>
        <div class="form-group">
          <label>
            Last name
            <input type="text" class="form-control" v-model="model.lastName" />
          </label>
        </div>
        <div class="form-group">
          <label>
            Date of birth
            <input
              type="date"
              class="form-control"
              v-model="model.dateOfBirth"
            />
          </label>
        </div>
        <div class="form-group">
          <label>
            Email
            <input type="email" class="form-control" v-model="model.email" />
          </label>
        </div>
        <button type="submit" class="btn btn-primary">Submit</button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "NewApplicant",
  data() {
    return {
      model: { firstName: "", lastName: "", dateOfBirth: null, email: "" }
    };
  },
  methods: {
    onSubmit() {
      this.$emit("submit", this.model);
    }
  }
};
</script>

<style scoped></style>
