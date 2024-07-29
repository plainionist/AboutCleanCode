<template>
  <div class="iframe-container">
    <iframe
      class="iframe"
      :src="noCacheUrl"
      :height="iFrameHeight"
      frameborder="0"
      scrolling="no"
      sandbox="allow-scripts allow-same-origin allow-popups allow-downloads"
    />
  </div>
</template>

<script setup>
  import { ref, computed, onMounted, onBeforeUnmount } from 'vue'

  const props = defineProps({
    url: String
  })

  const iFrameHeight = ref('100%')

  const noCacheUrl = computed(() => {
    const url = new URL(props.url)
    url.searchParams.append('cid', Math.round(Math.random() * 10000000))
    return url.toString()
  })
</script>
