const VisitorChart = {
  extends: VueChartJs.Bar,
  props: ["visitorDataProp"],
  data: function () {
    return {
      visitorData: this.visitorDataProp,
      options: {
        responsive: true, // Ensure the chart is responsive
        maintainAspectRatio: false, // Allow the chart to resize vertically
        scales: {
          xAxes: [
            {
              display: true,
            },
          ],
          yAxes: [
            {
              ticks: {
                beginAtZero: true,
                stepSize: 1,
                suggestedMin: 0,
              },
            },
          ],
        },
        legend: {
          display: true,
        },
      },
    };
  },
  mounted() {
    console.log("VisitorChart mounted");
    this.renderChart(this.visitorData, this.options);
    window.addEventListener("resize", this.updateChart); // Add a resize event listener
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.updateChart); // Remove the resize event listener
  },
  methods: {
    updateChart() {
      this._chart.update(); // Update the chart
    },
  },
  watch: {
    visitorDataProp(newVal) {
      console.log("visitorDataProp changed in VisitorChart", newVal);
      this.visitorData = newVal;
      this.renderChart(this.visitorData, this.options);
    },
  },
};

new Vue({
  el: "#visitor-chart",
  components: { VisitorChart },
  data: {
    visitorData: {}, // Initialize as an empty object
  },
  methods: {
    fetchVisitorData(startDate, endDate) {
      console.log("fetchVisitorData called", startDate, endDate);
      fetch(
        "/reports/getweeklyvisitors?startDate=" +
          startDate.toISOString() +
          "&endDate=" +
          endDate.toISOString()
      )
        .then((response) => response.json())
        .then((visitorCount) => {
          console.log("fetchVisitorData response", visitorCount);
          const data = [
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday",
          ].map((day) => visitorCount[day] || 0);
          this.visitorData = {
            labels: ["Lun", "Mar", "Mer", "Gio", "Ven", "Sab", "Dom"],
            datasets: [
              {
                label: "Numero di visitatori",
                backgroundColor: "#f87979",
                data: data,
              },
            ],
          };
        });
    },
  },
  mounted() {
    console.log("Vue instance mounted");
    var weekPicker = document.getElementById("weekPicker");
    weekPicker.addEventListener(
      "change",
      function (event) {
        console.log("weekPicker change event", event.target.value);
        var weekInfo = event.target.value.split("-");
        var year = weekInfo[0];
        var week = weekInfo[1].substring(1);
        var startDate = new Date(year, 0, 1 + (week - 1) * 7);
        var endDate = new Date(startDate);
        endDate.setDate(endDate.getDate() + 6);
        this.fetchVisitorData(startDate, endDate);
      }.bind(this)
    );
  },
});
