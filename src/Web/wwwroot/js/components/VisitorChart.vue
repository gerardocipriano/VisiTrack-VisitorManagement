<template>
  <div>
    <canvas id="visitorChart"></canvas>
  </div>
</template>

<script>
import { Bar } from "vue-chartjs";

export default {
  extends: Bar,
  props: ["visitorData"],
  mounted() {
    this.renderChart(this.visitorData, {
      responsive: true,
      maintainAspectRatio: false,
    });
  },
};
</script>
