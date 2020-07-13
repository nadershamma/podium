<template>
  <div class="card">
    <div class="card-body">
      <h2 class="card-title">Mortgages</h2>

      <div class="row" v-for="mortgage in getMortgages" :key="mortgage.id">
        <div class="col">
          <p>Lender: {{ mortgage.lender }}</p>
        </div>
        <div class="col">
          <p>Interest Rate: {{ mortgage.rate }}%</p>
        </div>
        <div class="col">
          <p>Type : {{ mortgage.type }}</p>
        </div>
        <div class="col">
          <p>LTV: &lt;{{ mortgage.loanToValue }}%</p>
        </div>
      </div>
    </div>
    <div class="card-footer">
      <button type="button" class="btn btn-primary" @click="onClick">
        Reset
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: "QualifyingMortgages",
  props: {
    mortgages: Array
  },
  computed: {
    getMortgages() {
      return this.mortgages;
    }
  },
  methods: {
    onClick() {
      this.$emit("reset");
    }
  }
};
</script>

<style scoped></style>
