import { Session } from "@/shared/types/Conference";

export const testSession: Session = {
  abstract: "test abstract",
  conferenceID: 0,
  duration: "test duration",
  endTime: new Date(),
  id: 0,
  speakers: [{ name: "test speaker", id: 0 }],
  startTime: new Date(),
  tags: [],
  track: {
    name: "test track name",
    conferenceID: 0,
    trackID: 0,
  },
  trackId: 0,
  title: "Test Session Title",
};
