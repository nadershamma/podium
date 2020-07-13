<template>
  <div class="card">
    <div class="card-body">
      <h2 class="card-title">Mortgage Lookup</h2>
      <form @submit.prevent="onSubmit">
        <div class="form-group">
          <label>
            Property Value
            <input
              type="number"
              class="form-control"
              v-model="model.propertyValue"
            />
          </label>
        </div>
        <div class="form-group">
          <label>
            Deposit Amount
            <input
              type="number"
              class="form-control"
              v-model="model.depositValue"
            />
          </label>
        </div>
        <button type="submit" class="btn btn-primary">Submit</button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "NewMortgages",
  data() {
    return {
      model: {
        propertyValue: 0,
        depositValue: 0
      }
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
