<template>
  <h1>MyApp</h1>

  <div>Welcome to the micro frontends demo</div>

  <div class="container">
    <FragmentContainer :url="featureA" />
    <FragmentContainer :url="featureB" />
  </div>
</template>

<script setup>
  import { FragmentContainer } from 'library'

  // In production we would retrieve these URIs from backend which 
  // would probably read those from some configuration.
  const featureA = 'http://localhost:7070'
  const featureB = 'http://localhost:6060'
</script>

<style scoped>
  .container {
    margin-top: 50px;
    display: grid;
    grid-template-columns: 1fr 1fr;
    width: 100%;
    gap: 10px;
  }
</style>
