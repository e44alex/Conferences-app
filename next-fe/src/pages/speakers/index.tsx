import { Speaker } from "@/shared/types/Conference";
import api from "@/shared/service/api";
import { SpeakerCard } from "@/components";
import useSwr from "swr";

const SpeakersPage = () => {
  const { data, isLoading, error } = useSwr<Speaker[]>("/", api.getSpeakers);

  if (error) return <p>{error.error}</p>;
  if (isLoading) return <p>Loading...</p>;

  return (
    <>
      <h1 className="title">Speakers list</h1>

      {data && (
        <div className="card-columns">
          {data.map((speaker) => (
            <SpeakerCard key={speaker.id} speaker={speaker} />
          ))}
        </div>
      )}
    </>
  );
};

export default SpeakersPage;
